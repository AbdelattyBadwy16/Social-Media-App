import React from 'react'
import { Outlet } from 'react-router'
import LeftNavbar from './NavBar/LeftNavbar'
import Navbar from './NavBar/Navbar'
import BottomMessenger from './Messenger/BottomMessenger'

export default function AppLayout() {
    return (
        <div className='flex'>
            <div>
                <LeftNavbar></LeftNavbar>
            </div>
            <div className='w-[100%]'>
                <Navbar></Navbar>
                <Outlet></Outlet>
                <BottomMessenger></BottomMessenger>
            </div>
        </div>
    )
}
