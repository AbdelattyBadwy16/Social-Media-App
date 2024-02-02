import React from 'react'
import { Outlet } from 'react-router'
import LeftNavbar from './LeftNavbar'
import Navbar from './Navbar'

export default function AppLayout() {
    return (
        <div className='flex'>
            <div>
                <LeftNavbar></LeftNavbar>
            </div>
            <div className='w-[100%]'>
                <Navbar></Navbar>
                <Outlet></Outlet>
            </div>
        </div>
    )
}
