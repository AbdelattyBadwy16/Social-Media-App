import React, { useState } from 'react'
import ProfileList from '../Helper/ProfileList';
import NotifyList from '../Helper/NotifyList';
import './Navbar.css'

export default function Navbar() {

    const [ActiveNotify, setActiveNotify] = useState(false);
    const [ActiveNotifyList, setActiveNotfiyList] = useState(false);
    const [ActiveProfile, setActiveProfile] = useState(false);
    return (

        <nav className='navbar w-[100%] h-[80px] bg-[white] flex items-center justify-around shadow-md'>
            <section className='flex items-center gap-6 w-[50%]'>
                <h1 className='font-bold text-[30px] '>MetaFans</h1>
                <div className="w-[40%] shadow-md p-2 rounded-lg flex bg-[#f8f8f8] ">
                    <input type='text' className='search  w-[90%] bg-[#f8f8f8] ' placeholder='Search'></input>
                    <img className='cursor-pointer' src="/icon/search.png" width={20}></img>
                </div>
            </section>
            <section className='flex gap-6'>
                <ul className='navlist flex gap-6'>
                    <li>Activity</li>
                    <li>Groups</li>
                    <li>Members</li>
                    <li>Photos</li>
                </ul>
                <img onClick={() => setActiveProfile(!ActiveProfile)} src='/image/profile.jpg' width={30} className='rounded-full cursor-pointer'></img>
                {
                    ActiveProfile ?
                        <ProfileList Active={ActiveProfile}></ProfileList> : ""
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
