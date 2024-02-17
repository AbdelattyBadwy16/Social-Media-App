import React, { useContext, useEffect, useState } from 'react'
import '/image/profile.jpg'
import { Link, useNavigate } from 'react-router-dom'
import './List.css'
import Cookies from 'universal-cookie';
import { GetUserPosts } from '../../Helper/PostApi';
import { ToastContainer, toast } from 'react-toastify';
import { User } from '@/Context/UserContext';


export default function ProfileList() {
    const cookie = new Cookies();
    const userName = cookie.get("userName");
    const image = cookie.get("image");
    const [topics, setTopics] = useState(0);
    const [leave , setLeave] = useState(false);
    const nav = useNavigate();
    // get user Posts
    useEffect(() => {
        async function fetch() {
            const data = await GetUserPosts();
            setTopics(data.length);
        }
        fetch();
    }, []);
    

      // logout
      function handelLogout() {
        cookie.remove("userName");
        cookie.remove("image");
        cookie.remove("bearer");
        cookie.remove("id");
        cookie.remove("PostId");
        cookie.remove("PostWindow");
        cookie.remove("PostStatus");
        toast("Logging out...");
        setTimeout(() => {
            nav("/login");
        }, 3000);

        return;
    }

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
                    <li onClick={handelLogout} className='w-[100%]'>
                        <Link to="" className='flex justify-around items-center'>
                            <h2>Logout</h2>
                        </Link>
                    </li>
                </ul>
            </section>
            <ToastContainer autoClose={2500} />
        </div>

    )
}
