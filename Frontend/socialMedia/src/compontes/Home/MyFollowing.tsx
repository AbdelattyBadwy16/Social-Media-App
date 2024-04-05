import { GetALLPosts, GetFollowingPosts } from '../../Helper/PostApi';
import React, {  useContext, useEffect } from 'react'
import Post from './Post';
import { UserPost } from '../../Context/UserPostContext';

export default function MyFollowing() {

    const context:any = useContext(UserPost);

    //Get All Posts
    useEffect(() => {
        async function fetch() {
            const res = await GetFollowingPosts();
            context.setPost([...res]);
        }
        fetch();
    }, [context.post]);

   
    return (
        <div className='posts w-ful flex flex-col gap-5'>

            {
                context.post.length ?
                    <section className='flex flex-col gap-5 mb-5'>
                        {
                            context.post.map((item : any) => <Post key={item.id} post={item}></Post>)
                        }
                    </section> :
                    <p className='text-center text-[30px] font-bold p-5 shadow-lg rounded-lg'>No Posts Have.</p>

            }
        </div>

    )
}

