import React, { useContext, useEffect, useState } from 'react'
import ProfileList from './ProfileList';
import NotifyList from './NotifyList';
import './Navbar.css'
import { Link } from 'react-router-dom';
import Cookies from 'universal-cookie';


export default function Navbar() {

    const [ActiveNotify, setActiveNotify] = useState(false);
    const [ActiveNotifyList, setActiveNotfiyList] = useState(false);
    const [ActiveProfile, setActiveProfile] = useState(false);
    const [ActivePage, setActivePage] = useState(1);
    const cookie = new Cookies();
    const IconImage = cookie.get("image");


    return (

        <nav className='navbar w-[100%] h-[80px] bg-[white] flex items-center justify-around shadow-md'>
            <section className='flex items-center gap-6 w-[50%]'>
                <h1 className='font-bold text-[30px] '>Glitchat</h1>
                <div className="search w-[40%] shadow-md p-2 rounded-lg flex bg-[#f8f8f8] ">
                    <input type='text' className='  w-[90%] bg-[#f8f8f8] ' placeholder='Search'></input>
                    <img className='cursor-pointer' src="/icon/search.png" width={20}></img>
                </div>
            </section>
            <section className='flex gap-6 items-center'>
                <ul className='navlist flex gap-6'>
                    <li><Link to="home" className={`${ActivePage === 1 ? "Active" : ""}`} onClick={() => setActivePage(1)}>Activity</Link></li>
                    <li><Link to="members" className={`${ActivePage === 3 ? "Active" : ""}`} onClick={() => setActivePage(3)}>Members</Link></li>
                    <li><Link to="photos" className={`${ActivePage === 4 ? "Active" : ""}`} onClick={() => setActivePage(4)}>Photos</Link></li>
                </ul>
                <div className='w-[35px] h-[35px]'>
                    <img onClick={() => setActiveProfile(!ActiveProfile)} src={`https://localhost:7279//userIcon/${IconImage}`} className='rounded-full cursor-pointer w-[100%] h-[100%]'></img>
                </div>
                {
                    ActiveProfile ?
                        <ProfileList ></ProfileList> : ""
                }
                {
                    ActiveNotify ?
                        <div onClick={() => setActiveNotfiyList(!ActiveNotifyList)} className='flex relative cursor-pointer'>
                            <img src='/icon/activeNotify.png' width={30} className='rounded-full'></img>
                            <div className='bg-red-500 w-3 h-3 rounded-full absolute right-0'></div>
                        </div> :
                        <div onClick={() => setActiveNotfiyList(!ActiveNotifyList)}>
                            <img src='/icon/notif.png' width={30} className='rounded-full cursor-pointer'></img>
                        </div>
                }
                {
                    ActiveNotifyList ?
                        <NotifyList /> : ""
                }
            </section>
        </nav>

    )
}
