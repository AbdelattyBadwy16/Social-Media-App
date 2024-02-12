import React, { useContext, useEffect, useState } from 'react'
import Blog from '../compontes/Home/Blog';
import Active from '../compontes/Home/Active';
import Group from '../compontes/Home/Group';
import './Home.css'
import { postWindow } from '../Context/PostWindow';
import Cookies from 'universal-cookie';
import NewPost from '../compontes/Profile/NewPost';
import { GetALLPosts } from '../Helper/PostApi';
import Post from '../compontes/Home/Post';

export default function Home() {

    const [ActiveFilter, SetActiveFilter] = useState(1);
    const [addNewPost, setAddNewPost] = useState(false);
    const [posts, setPosts] = useState([]);
    const cookie = new Cookies();

    //Get All Posts
    useEffect(() => {
        async function fetch() {
            const res = await GetALLPosts();
            setPosts(res);
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


        <div className='container flex gap-10 items-start justify-center w-[100%]'>
            {/*blog part*/}
            <Blog></Blog>

            {/*post part*/}
            <div className='posts w-[50%] flex flex-col gap-5'>

                <section onClick={handelAddPost} className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                    <section className='flex items-center gap-5'>
                        <img src="/image/Abdo.jpg" className="rounded-full" width={30}></img>
                        <h3 className='text-gray-500'>What's on your mind?</h3>
                    </section>
                    <section className='bg-gray-300 rounded-full p-1 border shadow-lg'>
                        <img src="/icon/MyPhoto.png" width={20} />
                    </section>
                </section>

                <section className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                    <ul className='flex gap-5'>
                        <li className={`${ActiveFilter === 1 ? "Active" : ""}`} onClick={() => SetActiveFilter(1)}>All Members</li>
                        <li className={`${ActiveFilter === 2 ? "Active" : ""}`} onClick={() => SetActiveFilter(2)}>My Groups</li>
                        <li className={`${ActiveFilter === 3 ? "Active" : ""}`} onClick={() => SetActiveFilter(3)}>My Favorites</li>
                        <li className={`mentions ${ActiveFilter === 4 ? "Active" : ""}`} onClick={() => SetActiveFilter(4)}>Mentions</li>
                    </ul>
                </section>

                {
                    posts.length ?
                        <section className='flex flex-col gap-5 mb-5'>
                            {
                                posts.map((item) => <Post key={item.id} post={item}></Post>)
                            }
                        </section> :
                        <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p>

                }
            </div>


            {/*Avtive and group part*/}
            <div className='active-group w-[25%] flex flex-col gap-5 sticky top-0'>
                <Active></Active>
                <Group></Group>
            </div>


            {
                PostCheck ?
                    <>
                        <NewPost></NewPost>
                    </> : ""
            }

            {
                PostCheck ?
                    <>
                        <div className='w-[100%] h-[5000px] z-[1000] top-0 left-0 bg-gray-500 opacity-80 rounded-lg absolute flex items-center justify-center'>
                        </div>
                    </> : ""
            }

        </div>
    )
}
