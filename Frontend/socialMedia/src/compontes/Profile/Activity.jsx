import React, { useContext, useEffect, useState } from 'react'
import Post from '../Home/Post'
import About from './About'
import PhotosList from './PhotosList'
import "./Activity.css"
import Cookies from 'universal-cookie'
import { GetUserData } from '../../Helper/ProfileApi'
import NewPost from './NewPost'
import { postWindow } from '../../Context/PostWindow'
import { GetUserPosts } from '../../Helper/PostApi'
import { UserPost } from "../../Context/UserPostContext"

export default function Activity() {

    const [data, setData] = useState({ userName: "", firstName: "", secondName: "", about: "", bitrhDate: "", email: "", country: "", phoneNumber: "", followers: 0, following: 0 });
    const [isLoading, setIsLoading] = useState(false);
    const [addNewPost, setAddNewPost] = useState(false);
    const cookie = new Cookies();
    const IconImage = cookie.get("image");
    // get user data
    useEffect(() => {
        setIsLoading(true);
        async function fetch() {
            try {
                const id = cookie.get("id");
                const data = await GetUserData(id);
                setData(data);
            } finally {
                setIsLoading(false);
            }
        }
        fetch();
    }, []);


    // get user Posts
    const context = useContext(UserPost);

    useEffect(() => {
        setIsLoading(true);
        async function fetch() {

            const data = await GetUserPosts();
            context.setPost(data);

        }
        fetch();
    }, []);


    //add post window
    const PostWindow = useContext(postWindow);
    async function handelAddPost() {
        cookie.remove("PostWindow")
        cookie.set("PostWindow", 'true');
        setAddNewPost(!addNewPost);
        PostWindow.setOpen("true");
        cookie.remove("PostStatus")
        cookie.set("PostStatus", "add");
        return;
    }
    const PostCheck = cookie.get("PostWindow");
    return (
        <>
            <div className=' m-auto  mt-10 flex gap-10 items-start justify-center w-[100%]' >

                {/*post part*/}
                <div className='posts w-[50%] flex flex-col gap-5' >

                    <section onClick={handelAddPost} className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                        <section className='flex items-center gap-5'>
                            <img src={`https://localhost:7279//userIcon/${IconImage}`} className="rounded-full" width={30}></img>
                            <h3 className='text-gray-500'>What's on your mind?</h3>
                        </section>
                        <section className='bg-gray-300 rounded-full p-1 border shadow-lg'>
                            <img src="/icon/MyPhoto.png" width={20} />
                        </section>
                    </section>

                    {context.post ?
                        context.post.length ?

                            <section className='flex flex-col gap-5'>
                                {
                                    context.post.map((item) => <Post key={item.id} post={item}></Post>)
                                }
                            </section> :
                            <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p> : ""
                    }

                </div >

                <section className="about-photo sticky top-0 w-[25%]">
                    <About about={data.about}></About>
                    <PhotosList></PhotosList>
                </section>

                {
                    PostCheck ?
                        <>
                            <NewPost></NewPost>
                        </> : ""
                }

                {
                    PostCheck ?
                        <>
                            <div className='w-[100%] h-[5000px] z-[1000] top-0 bg-gray-500 opacity-80 rounded-lg absolute flex items-center justify-center left-0'>
                            </div>
                        </> : ""
                }

            </div >


        </>
    )
}
