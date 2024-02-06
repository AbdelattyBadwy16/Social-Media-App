import React, { useContext, useEffect, useState } from 'react'
import '/image/profile.jpg'
import { Link } from 'react-router-dom'
import './List.css'
import Cookies from 'universal-cookie';
import { UserPost } from '../../Context/UserPostContext';
import { GetUserPosts } from '../../Helper/PostApi';


export default function ProfileList() {
    const cookie = new Cookies();
    const userName = cookie.get("userName");
    const image = cookie.get("image");
    const [topics, setTopics] = useState(0);
    const [leave , setLeave] = useState(false);

    // get user Posts
    useEffect(() => {
        async function fetch() {
            const data = await GetUserPosts();
            setTopics(data.length);
        }
        fetch();
    }, []);
    

    return (

        <div className={`${leave ? "hidden" : ""}  w-[200px] bg-[white]  shadow-lg border border-gray-200 absolute z-[999] right-20 top-20`}>
            <section className='p-5 bg-gray-200 rounded-lg flex gap-5'>
                <img src={`https://localhost:7279//userIcon/${image}`} width={30} className='rounded-full'></img>
                <div>
                    <h5>Hello, {userName}</h5>
                    <p className='text-[10px] text-gray-500'>Community Head</p>
                </div>
            </section>
            <section>
                <ul className='ProList flex flex-col gap-3 mt-5 mb-5 items-center'>
                    <li className='w-[100%]'>
                        <Link to="/profile" className='flex justify-around items-center'>
                            <h2>Topics</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>{topics}</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Groups</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>3</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Friends</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>5</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Messages</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>0</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Setting</h2>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Logout</h2>
                        </Link>
                    </li>
                </ul>
            </section>
        </div>

    )
}
