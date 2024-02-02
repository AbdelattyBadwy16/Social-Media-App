import React, { useState } from 'react'
import '/image/profile.jpg'
import { Link } from 'react-router-dom'
import './List.css'


export default function ProfileList() {


    return (

        <div className='w-[200px] bg-[white]  shadow-lg border border-gray-200 absolute z-[999] right-20 top-20'>
            <section className='p-5 bg-gray-200 rounded-lg flex gap-5'>
                <img src='/image/profile.jpg' width={30} className='rounded-full'></img>
                <div>
                    <h5>Hello, Abdelatty</h5>
                    <p className='text-[10px] text-gray-500'>Community Head</p>
                </div>
            </section>
            <section>
                <ul className='ProList flex flex-col gap-3 mt-5 mb-5 items-center'>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Profile</h2>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Topics</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>3</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Groups</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>3</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Friends</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>5</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Messages</h2>
                            <div className='bg-gray-200 text-blue-600 rounded-full text-[white] pl-1 pr-1'>
                                <p>0</p>
                            </div>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Setting</h2>
                        </Link>
                    </li>
                    <li className='w-[100%]'>
                        <Link className='flex justify-around items-center'>
                            <h2>Logout</h2>
                        </Link>
                    </li>
                </ul>
            </section>
        </div>

    )
}
