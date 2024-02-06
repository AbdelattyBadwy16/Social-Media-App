import Cookies from 'universal-cookie'
import { GetUserData } from '../../Helper/ProfileApi'
import React, { useContext, useEffect, useState } from 'react'
import { ComputeDate } from '../../Helper/ComputeDate'
import { CheckPostReact, DeletePost, GetPost, GetUserPosts, RemovePostReact, UpdateReacts } from '../../Helper/PostApi'
import { UserPost } from '../../Context/UserPostContext'
import { postWindow } from '../../Context/PostWindow'

interface Post {
    firstName: string
    secondName: string
    createdAt: string
    content: string
    likes: number
    loves: number
    sads: number
    haha: number
    angry: number
    wow: number
    status: string
    id: number
}

export default function Post(CurPost: Post) {

    const cookie = new Cookies();
    const image = cookie.get("image");

    const [post, setPost] = useState<Post>({});
    const [firstName, setFirstName] = useState("");
    const [secondName, setSecondName] = useState("");
    const [openPostList, setOpenPostList] = useState(false);
    const [openReactList, setOpenReactList] = useState(false);
    const [reactType, setReactType] = useState("");
    // user Details
    useEffect(() => {
        setPost(CurPost.post);

        async function fetch() {

            const data = await GetUserData();
            setFirstName(data.firstName);
            setSecondName(data.lastName);
            const res = await CheckPostReact(CurPost?.post.id);
            setReactType(res);
        }
        fetch();
    }, []);

    // convert time
    const ago = ComputeDate(post?.createdAt);
    const PostDate = new Date(post?.createdAt);
    var PostTime = "";
    if (ago === "year") {
        let time = new Date().getFullYear();
        PostTime = `${time - PostDate.getFullYear()} years ago`;
    } else if (ago === "month") {
        let time = new Date().getMonth();
        PostTime = `${time - PostDate.getMonth()} Months ago`;
    } else if (ago === "day") {
        let time = new Date().getDay();
        PostTime = `${time - PostDate.getDay()} Days ago`;
    } else if (ago === "hours") {
        let time = new Date().getHours();
        PostTime = `${time - PostDate.getHours()} Hours ago`;
    } else if (ago === "minuts") {
        let time = new Date().getMinutes();
        if (time - PostDate.getMinutes() == 0) PostTime = "Now";
        else if (time - PostDate.getMinutes() == 1) PostTime = `${time - PostDate.getMinutes()} Minute ago`;
        else
            PostTime = `${time - PostDate.getMinutes()} Minutes ago`;
    }

    //Delete Post
    const context = useContext(UserPost);
    async function handelDelete() {
        const res = await DeletePost(post.id);
        setOpenPostList(false);
        const data = await GetUserPosts();
        context.setPost(data);
        return;
    }


    //add react
    async function handelAddReact(type: string) {
        const res = await UpdateReacts(post.id, type);
        const data = await GetPost(post.id);
        setPost(data);
        setReactType(type);
        return;
    }

    //remove react
    async function RemoveReact() {
        const res = await RemovePostReact(post.id);
        const data = await GetPost(post.id);
        setPost(data);
        setReactType("0");
    }


    //edit post
    const PostWindow = useContext(postWindow);
    async function handelEdit() {
        cookie.set("PostWindow", 'true');
        PostWindow.setOpen("true");
        cookie.set("PostStatus","edit");
        cookie.set("PostId",post.id);
        setOpenPostList(false);
        return;
    }
    const PostCheck = cookie.get("PostWindow");
    return (
        <div className='bg-[white] border shadow-lg rounded-lg'>

            <div className='flex items-center justify-between relative p-5'>
                <div className='flex gap-5'>
                    <img src={`https://localhost:7279//userIcon/${image}`} className="rounded-full" width={30}></img>
                    <div>
                        <p> <span className='font-bold'>{`${firstName} ${secondName}`}</span> posted an update</p>
                        <div className='flex gap-3 items-center'>
                            <p className='text-[12px] text-gray-500'>{PostTime}</p>
                            <div className='flex items-center gap-1'>
                                {
                                    post?.status === "public" ?
                                        <img src="/icon/public.png" width={15}></img> :
                                        <img src="/icon/private.png" width={15}></img>
                                }
                                <p className='text-[12px] text-gray-500'>{post?.status}</p>

                            </div>
                        </div>
                    </div>
                </div>
                <div onClick={() => setOpenPostList(!openPostList)} className='text-[30px] cursor-pointer'>...</div>
                {
                    openPostList ?
                        <ul className=' shadow-lg bg-gray-300 transition-all flex flex-col gap-3 rounded-md absolute right-5 top-[60px]'>
                            <li onClick={handelDelete} className='cursor-pointer hover:bg-gray-400 p-2 rounded-md'>Delete</li>
                            <li onClick={handelEdit} className='cursor-pointer hover:bg-gray-400 p-2 rounded-md'>Edit</li>
                            <li className='cursor-pointer  hover:bg-gray-400 p-2 rounded-md'>Save at favourite</li>
                        </ul> : ""
                }
            </div>

            <div className=' p-5'>
                <p className='mb-5'>{post?.content !== undefined ? post?.content : ""}</p>
                <img src="/image/Abdo.jpg"  className='w-[100%] max-h-[500px]'></img>
                <div className='flex item-center justify-between mb-1 mt-10'>
                    <div className='flex transition  duration-1000 '>
                        {post?.likes + post?.angry + post?.loves + post?.sads + post?.haha + post?.wow}
                        <img className={`${post?.likes ? "" : "absolute hidden"} cursor-pointer rounded-full bg-red-500 `} src="/icon/like.png" width={20}></img>
                        <img className={`${post?.loves ? "" : "absolute hidden"} cursor-pointer rounded-full relative bg-blue-500 `} src="/icon/love.png" width={20}></img>
                        <img className={`${post?.haha ? "" : "absolute hidden"} cursor-pointer rounded-full relative bg-red-500 `} src="/icon/haha.png" width={20}></img>
                        <img className={`${post?.wow ? "" : "absolute hidden"} cursor-pointer rounded-full relative bg-blue-500 `} src="/icon/wow.png" width={20}></img>
                        <img className={`${post?.sads ? "" : "absolute hidden"} cursor-pointer rounded-full relative bg-red-500`} src="/icon/sad.png" width={20}></img>
                        <img className={`${post?.angry ? "" : "absolute hidden"} cursor-pointer rounded-full relative bg-blue-500 `} src="/icon/angry.png" width={20}></img>
                    </div>
                    <div>
                        0 comments
                    </div>
                </div>

                <hr></hr>

                <div className='flex gap-5 mt-2  justify-between'>
                    <div className='flex gap-5 relative'>
                        <div onMouseOver={() => setOpenReactList(true)} onMouseLeave={() => setOpenReactList(false)} className='flex gap-2 items-center cursor-pointer rounded-lg hover:bg-gray-300 p-2'>
                            <p>{reactType === "0" ? <img src="/icon/blackLike.png" width={20}></img> : <img onClick={RemoveReact} width={40} className='bg-gray-300 shadow-lg rounded-lg' src={`/icon/${reactType}.png`} />}</p>
                        </div>
                        {
                            openReactList ?
                                <div onMouseOver={() => setOpenReactList(true)} onMouseLeave={() => setOpenReactList(false)} className='bg-gray-300 shadow-lg rounded-lg absolute bottom-10 flex gap-2 p-2'>
                                    <img onClick={() => {
                                        let t = "like";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/like.png" width={20}></img>
                                    <img onClick={() => {
                                        let t = "love";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/love.png" width={20}></img>
                                    <img onClick={() => {
                                        let t = "haha";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/haha.png" width={20}></img>
                                    <img onClick={() => {
                                        let t = "wow";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/wow.png" width={20}></img>
                                    <img onClick={() => {
                                        let t = "sad";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/sad.png" width={20}></img>
                                    <img onClick={() => {
                                        let t = "angry";
                                        handelAddReact(t);
                                    }} className='cursor-pointer hover:bg-gray-400 rounded-lg' src="/icon/angry.png" width={20}></img>
                                </div> : ""
                        }
                        <div className='flex gap-2 items-center cursor-pointer rounded-lg hover:bg-gray-300 p-2'>
                            <img src="/icon/comment.png" width={20}></img>
                            <p>Comment</p>
                        </div>
                    </div>
                    <div className='flex gap-2 cursor-pointer rounded-lg hover:bg-gray-300 p-2'>
                        <img src="/icon/share.png" width={20}></img>
                        <p>share</p>
                    </div>
                </div>
            </div>

            <div className='bg-[#f2f2f2] p-5 w-[100%]'>
                <div className='flex gap-5 w-[100%]'>
                    <img src={`https://localhost:7279//userIcon/${image}`} className='rounded-full' width={30}></img>
                    <div className="w-[40%] shadow-md p-2 rounded-lg flex bg-[white] ">
                        <input type='text' className='search  w-[100%]' placeholder='Type a comment...'></input>
                        <img className='cursor-pointer' src="/icon/inbox.png" width={20}></img>
                    </div>
                </div>
            </div>

        </div>
    )
}
