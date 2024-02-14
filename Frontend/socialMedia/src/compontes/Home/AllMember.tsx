import { GetALLPosts } from '../../Helper/PostApi';
import React, {  useEffect, useState } from 'react'
import Post from '../../compontes/Home/Post';

export default function AllMember() {

    const [posts, setPosts] = useState([]);

    //Get All Posts
    useEffect(() => {
        async function fetch() {
            const res = await GetALLPosts();
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
                            posts.map((item) => <Post key={item.id} post={item}></Post>)
                        }
                    </section> :
                    <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p>

            }
        </div>

    )
}

