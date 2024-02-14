import {  GetFavPost } from '../../Helper/PostApi';
import React, { useEffect, useState } from 'react'
import Post from '../../compontes/Home/Post';
import Cookies from 'universal-cookie';

export default function MyFavourite() {

    const [posts, setPosts] = useState([]);
    const cookie = new Cookies();
    const userId = cookie.get("id");
    const [data, setData] = useState([]);
    //Get All Posts
    useEffect(() => {
        async function fetch() {
            const res = await GetFavPost(userId);
            setData(res);
            setPosts(res);
        }
        fetch();
    }, []);


    return (
        <div className='posts w-ful flex flex-col gap-5'>

            {
                posts.length ?
                    <section className='flex flex-col gap-5 mb-5'>
                        {
                            data.map((item) => <Post key={item.id} post={item.post}></Post>)
                        }
                    </section> :
                    <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p>

            }
        </div>

    )
}

