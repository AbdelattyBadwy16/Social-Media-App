import React, { useContext, useEffect, useState } from 'react'
import "./NewPost.css"
import Cookies from 'universal-cookie';
import { postWindow } from '../../Context/PostWindow';
import { GetUserData } from '../../Helper/ProfileApi';
import { CreateNewPost, GetPost, GetUserPosts, UpdatePost } from '../../Helper/PostApi';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { UserPost } from '../../Context/UserPostContext';

export default function NewPost() {
    const cookie = new Cookies();
    const [content, SetContent] = useState("");
    const [status, SetStatus] = useState("public");
    const [addPhoto, setAddPhoto] = useState(false);
    const image = cookie.get("image");
    // check operation
    const operation = cookie.get("PostStatus");
    if (operation === "edit") {
        const postId = cookie.get("PostId");
        useEffect(() => {

            async function fetch() {
                const data = await GetPost(postId);
                SetStatus(data.status);
                SetContent(data.content);
            }
            fetch();

        }, []);

    }

    //handel close post
    const PostWindow = useContext(postWindow);
    function handelClosePost() {
        cookie.set("PostWindow", 'false');
        PostWindow.setOpen(false);
        return;
    }

    //handel add post or update
    const context = useContext(UserPost)

    async function handelSubmit() {
       
        var f = document.getElementById("form");
        const newForm = new FormData(f);

        let userId = await GetUserData();
        userId = userId.id;
        if (operation === "edit") {
            const res = await UpdatePost(content, status);

            toast("Post Edited Successfuly!");
            const data = await GetUserPosts();
            context.setPost([...data]);

        } else {
            const res = await CreateNewPost({ content, userId, status , file : newForm });
            console.log(res);
            if (res.ok) {
                toast("Post Created Successfuly!");
                const data = await GetUserPosts();
                context.setPost(data);
            }
        }

        setTimeout(() => {
            cookie.set("PostWindow", 'false');
            PostWindow.setOpen(false);
        }, 2000);


        return;
    }


    // user Details
    const [name, setName] = useState("");
    useEffect(() => {

        async function fetch() {
            const data = await GetUserData();
            setName(data.firstName + " " + data.lastName);
        }
        fetch();
    }, []);


   


    return (
        <>
            <div className='newpost w-[50%] max-h-[10%] h-[30%] rounded-lg absolute z-[1001] bg-[white] opacity-100'>
                <div className='flex gap-0 items-center justify-between p-5 pb-0' >
                    <p className='close p-2 cursor-pointer rounded-full font-bold' onClick={handelClosePost}>X</p>
                </div>
                <div className='text-center font-bold text-[20px] mb-5 absolute top-5 left-[38%]'>{operation === "edit" ? "Edit Post" : "Create New Post"}</div>
                <hr></hr>
                <div className='p-5 flex gap-5 bg-[white]'>
                    <img src={`https://localhost:7279//userIcon/${image}`} width={60} className='rounded-full'></img>
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
                <div className='flex p-5 justify-between rounded-b-lg bg-[white]'>
                    <button onClick={handelSubmit} className='bg-gray-400 shadow-lg p-1 rounded-lg font-bold w-[20%]'>{operation === "edit" ? "Edit" : "Posted"}</button>
                    <div className='flex gap-5'>
                        <img src="/icon/MyPhoto.png" className='cursor-pointer' onClick={() => setAddPhoto(!addPhoto)} width={30}></img>
                        <form id="form">
                            <input name="image" id='file' type='file' className={`${addPhoto ? "" : "hidden"} w-[100px]`}></input>
                        </form>
                    </div>
                </div>
            </div>
            <ToastContainer autoClose={1500} />
        </>
    )
}
