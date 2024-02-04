import React, { useState } from 'react'
import '/icon/allList.png'
import '/icon/timeline.png'
import '/icon/MyProfile.png'
import '/icon/Notification.png'
import '/icon/MyPhoto.png'
import '/icon/inbox.png'
import '/icon/myFriends.png'
import '/icon/MyGroup.png'
import '/icon/setting.png'
import '/icon/Logout.png'
import { Link } from 'react-router-dom'

export default function LeftNavbar() {
    const [Active, SetActive] = useState(false);
    return (
        <div className={`leftNavBar ${!Active ? 'w-[5%]' : 'w-[10%]'} bg-[white] h-[100vh] shadow-lg transition-all z-[1000] flex flex-col items-center fixed top-0`}>
            <img onClick={() => SetActive(!Active)} src='/icon/allList.png' width={25} className='mt-10 cursor-pointer'></img>
            <div className='flex flex-col gap-10 mt-20'>
                <Link to="home" className='leftNavItem flex gap-3'>
                    <img src='/icon/timeline.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>TimeLine</p>
                </Link>
                <Link to="profile" className='leftNavItem flex gap-3'>
                    <img src='/icon/MyProfile.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>MyProfile</p>
                </Link>
                <Link to="" className='leftNavItem flex gap-3'>
                    <img src='/icon/Notification.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>Notification</p>
                </Link>
                <Link to="photos" className='leftNavItem flex gap-3'>
                    <img src='/icon/MyPhoto.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>MyPhotos</p>
                </Link>
                <Link to="" className='leftNavItem flex gap-3'>
                    <img src='/icon/inbox.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>inbox</p>
                </Link>
                <Link to="groups" className='leftNavItem flex gap-3'>
                    <img src='/icon/MyGroup.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>MyGroups</p>
                </Link>
                <Link to="" className='leftNavItem flex gap-3'>
                    <img src='/icon/setting.png' width={20}></img>
                    <p className={`${Active ? "" : "hidden"}`}>Setting</p>
                </Link>
            </div>
            <Link to="login" className='leftNavItem flex mt-[50px] gap-3'>
                <img src="/icon/Logout.png" className='cursor-pointer' width={25}></img>
                <p className={`${Active ? "" : "hidden"}`}>Logout</p>
            </Link>
        </div>
    )
}
