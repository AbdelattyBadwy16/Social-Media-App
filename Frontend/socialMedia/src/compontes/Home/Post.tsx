import Cookies from 'universal-cookie'
import { GetUserData } from '../../Helper/ProfileApi'
import React, { useContext, useEffect, useState } from 'react'
import { ComputeDate } from '../../Helper/ComputeDate'
import { AddComment, AddFavPost, CheckFavPost, CheckPostReact, DeleteFavPost, DeletePost, GetFavPost, GetPost, GetPostComments, GetUserPosts, RemoveComment, RemovePostReact, UpdateReacts } from '../../Helper/PostApi'
import { UserPost } from '../../Context/UserPostContext'
import { postWindow } from '../../Context/PostWindow'
import { FavPost } from '../../Context/MyFavPost'

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
    id: number,
    userId: string,
    imagePath: string
}

export default function Post(CurPost: any) {

    const cookie = new Cookies();
    const userImage = cookie.get("image");
    const userId = cookie.get("id");
    const [post, setPost] = useState<Post>({});
    const [firstName, setFirstName] = useState("");
    const [secondName, setSecondName] = useState("");
    const [openPostList, setOpenPostList] = useState(false);
    const [openReactList, setOpenReactList] = useState(false);
    const [reactType, setReactType] = useState("");
    const [commentList, setcommentList] = useState(false);
    const [comments, setComments] = useState([]);
    const [comment, setComment] = useState("");
    const [image, setImage] = useState("");
    const [isFav, setIsFav] = useState(false);
    // user Details
    useEffect(() => {
        setPost(CurPost.post);
        async function fetch() {

            //Get User Data
            const id = CurPost.post.userId;
            const data = await GetUserData(id);
            setFirstName(data.firstName);
            setSecondName(data.lastName);
            setImage(data.iconImagePath);

            //Get Post React
            const res: any = await CheckPostReact(CurPost?.post.id);
            setReactType(res);

            // Get Post Comment
            const comments = await GetPostComments(CurPost?.post.id);
            setComments(comments)

            //Check IF in Fav or not
            const fav = await CheckFavPost(CurPost?.post.id, userId);
            if (fav == "Found") setIsFav(true);
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
    const context: any = useContext(UserPost);
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
    const PostWindow : any = useContext(postWindow);
    async function handelEdit() {
        cookie.remove("PostWindow")
        cookie.set("PostWindow", 'true');
        PostWindow.setOpen("true");
        cookie.remove("PostStatus");
        cookie.remove("PostId");
        cookie.set("PostStatus", "edit");
        cookie.set("PostId", post.id);
        setOpenPostList(false);
        return;
    }

    //handel Add Comment
    async function handelAddComment(e: any) {
        if (!comment.length || e.key != "Enter") return;
        const res = await AddComment(post.id, comment);
        setcommentList(true);
        setComment("");
        handelCommentList();
        return;
    }

    //handelDeleteComment

    async function handelDeleteComment() {
        const res = await RemoveComment(post.id);
        handelCommentList();
        return;
    }

    //handel Comment List
    async function handelCommentList() {

        const res = await GetPostComments(post.id);
        setcommentList(!commentList);
        // this mean commient list is closed so no need to fetch data 
        if (commentList == true) return;
        setComment("");
        setComments(res);
        return;
    }


    //handel Add To Favourite
    const context2 : any = useContext(FavPost);
    async function handelAddToFav() {
        setIsFav(true);
        setOpenPostList(false);
        const userId = cookie.get("id");
        const res = await AddFavPost(post.id, userId);
        const data = await GetFavPost(userId);
        context2.setPost(data);
    }


    //handel Delete Favourite
    async function handelDeleteToFav() {
        setIsFav(false);
        setOpenPostList(false);
        const userId = cookie.get("id");
        const res = await DeleteFavPost(post.id, userId);
        const data = await GetFavPost(userId);
        context2.setPost(data);
    }

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
                            <li onClick={handelDelete} className={`cursor-pointer hover:bg-gray-400 p-2 rounded-md ${post.userId == userId ? "" : "hidden"}`}>Delete</li>
                            <li onClick={handelEdit} className={`cursor-pointer hover:bg-gray-400 p-2 rounded-md ${post.userId == userId ? "" : "hidden"}`}>Edit</li>
                            {
                                isFav ? <li onClick={handelDeleteToFav} className='cursor-pointer bg-blue-400 hover:bg-blue-800 hover:text-white p-2 rounded-md'>Delete from favourite</li> :
                                    <li onClick={handelAddToFav} className='cursor-pointer  hover:bg-gray-400 p-2 rounded-md'>Save at favourite</li>
                            }
                        </ul> : ""
                }
            </div>

            <div className=' p-5'>
                <p className='mb-5'>{post?.content !== undefined ? post?.content : ""}</p>
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
                        {comments.length} comments
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
                        <label htmlFor={post.id} className='flex gap-2 items-center cursor-pointer rounded-lg hover:bg-gray-300 p-2'>
                            <img src="/icon/comment.png" width={20}></img>
                            <p>Comment</p>
                        </label>
                    </div>

                </div>
            </div>

            <div className='bg-[#f2f2f2] flex flex-col  gap-5 p-5 w-[100%]'>
                <div className='flex gap-5  w-[100%]'>
                    <img src={`https://localhost:7279//userIcon/${userImage}`} className='rounded-full' width={30}></img>
                    <div className="w-[100%] justify-between shadow-md p-2 rounded-lg flex bg-[white] gap-5">
                        <input id={post.id} name={post.id} onKeyPress={handelAddComment} value={comment} onChange={(e) => setComment(e.target.value)} type='text' className='w-[100%]' placeholder='Type a comment...'></input>
                    </div>
                    <div onClick={handelCommentList} className='text-[20px] cursor-pointer p-3 rounded-full'>{!commentList ? "v" : "^"}</div>
                </div>
                {
                    commentList ?
                        <div>
                            <hr></hr>
                            <div className='flex flex-col '>
                                {
                                    comments.map((item) =>
                                        <div key={item.id} className='mr-10 flex gap-5 items-center shadow-lg p-2 rounded-lg w-full' >
                                            <div className='flex items-center justify-between p-3 w-full'>
                                                <div className='flex gap-2 items-center'>
                                                    <img className='w-[40px] rounded-full' src={`https://localhost:7279//userIcon/${item.userImagePath}`}></img>
                                                    <div>
                                                        <h3 className='font-bold'>{item.userName}</h3>
                                                        <p>{item.content}</p>
                                                    </div>
                                                </div>
                                                <div onClick={handelDeleteComment} className='hover:bg-gray-400 p-2 rounded-full cursor-pointer'>X</div>
                                            </div>
                                        </div>
                                    )

                                }
                            </div>
                        </div>
                        : ""
                }
            </div>

        </div >
    )
}
