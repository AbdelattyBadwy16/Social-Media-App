import { GetFavPost } from '../../Helper/PostApi';
import React, { useContext, useEffect, useState } from 'react'
import Post from './Post';
import Cookies from 'universal-cookie';
import { FavPost } from '../../Context/MyFavPost'

export default function MyFavourite() {

    const [posts, setPosts] = useState([]);
    const cookie = new Cookies();
    const userId = cookie.get("id");
    const [data, setData] = useState([]);
    const context : any = useContext(FavPost);

    //Get All Posts
    useEffect(() => {
        async function fetch() {
            const res = await GetFavPost(userId);
            setData(res);
            setPosts(res);
            context.setPost(res);
            setPosts(context.post);
        }
        fetch();
    }, [context.post]);


    return (
        <div className='posts w-ful flex flex-col gap-5'>

            {
                posts.length ?
                    <section className='flex flex-col gap-5 mb-5'>
                        {
                            data.map((item : any) => <Post key={item.id} post={item.post}></Post>)
                        }
                    </section> :
                    <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p>

            }
        </div>

    )
}

