import React, { useContext, useEffect, useState } from 'react'
import "./NewPost.css"
import Cookies from 'universal-cookie';
import { postWindow } from '../../Context/PostWindow';
import { GetUserData } from '../../Helper/ProfileApi';
import { CreateNewPost, GetALLPosts, GetUserPosts } from '../../Helper/PostApi';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { UserPost } from '../../Context/UserPostContext';

export default function NewPost() {

    //handel close post
    const cookie = new Cookies();
    const PostWindow = useContext(postWindow);

    function handelClosePost() {
        cookie.set("PostWindow", 'false');
        PostWindow.setOpen(false);
        return;
    }

    //handel add post
    const context = useContext(UserPost)
    const [content, SetContent] = useState("");
    const [status, SetStatus] = useState("public");
    async function handelSubmit() {
        const token = cookie.get("bearer");
        const username = cookie.get("userName");
        let userId = await GetUserData({ token, username });
        userId = userId.id;
        const res = await CreateNewPost({ content, userId, status });
        if (res.ok) {
            toast("Post Created Successfuly!");
            const data = await GetUserPosts();
            context.setPost(data);
        }

        setTimeout(() => {
            cookie.set("PostWindow", 'false');
            PostWindow.setOpen(false);
        }, 2000);


        return;
    }


     // user Details
     const [name ,setName] = useState("");
     useEffect(() => {

        async function fetch() {

            const data = await GetUserData();
            setName(data.firstName + " " + data.lastName);
        }
        fetch();
    }, []);


    return (
        <>
            <div className='newpost w-[50%] h-[30%] rounded-lg absolute z-[1001] bg-[white] opacity-100'>
                <div className='flex gap-0 items-center justify-between p-5 pb-0' >
                    <p className='close p-2 cursor-pointer rounded-full font-bold' onClick={handelClosePost}>X</p>
                </div>
                <div className='text-center font-bold text-[20px] mb-5 absolute top-5 left-[38%]'>Create New Post</div>
                <hr></hr>
                <div className='p-5 flex gap-5 bg-[white]'>
                    <img src='/image/Abdo.jpg' width={60} className='rounded-full'></img>
                    <div>
                        <h1 className='font-bold mb-2'>{name}</h1>
                        <select value={status} onChange={(e) => SetStatus(e.target.value)} className='rounded-lg shadow-lg p-1'>
                            <option value="public">Public</option>
                            <option value="private">Private</option>
                        </select>
                    </div>
                </div>
                <div className='h-[100%]'>
                    <textarea value={content} onChange={(e) => SetContent(e.target.value)} className='p-5 w-[100%] h-[100%]' placeholder={`What are you thinking, ${name}?`}></textarea>
                </div>
                <div className='flex p-2 justify-between rounded-b-lg bg-[white]'>
                    <button onClick={handelSubmit} className='bg-gray-400 shadow-lg p-1 rounded-lg font-bold w-[20%]'>Posted</button>
                    <img src="/icon/MyPhoto.png" width={30}></img>
                </div>
            </div>
            <ToastContainer autoClose={1500} />
        </>
    )
}
