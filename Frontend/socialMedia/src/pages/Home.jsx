import React, { useState } from 'react'
import Blog from '../compontes/Home/Blog';
import Post from '../compontes/Home/Post';
import Active from '../compontes/Home/Active';
import Group from '../compontes/Home/Group';
import './Home.css'

export default function Home() {

    const [ActiveFilter, SetActiveFilter] = useState(1);

    return (


        <div className='container flex gap-10 items-start justify-center relative w-[100%]'>
            {/*blog part*/}
            <Blog></Blog>

            {/*post part*/}
            <div className='posts w-[50%] flex flex-col gap-5'>

                <section className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                    <section className='flex items-center gap-5'>
                        <img src="/image/Abdo.jpg" className="rounded-full" width={30}></img>
                        <h3 className='text-gray-500'>What's on your mind?</h3>
                    </section>
                    <section className='bg-gray-300 rounded-full p-1 border shadow-lg'>
                        <img src="/icon/MyPhoto.png" width={20} />
                    </section>
                </section>

                <section className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                    <ul className='flex gap-5'>
                        <li className={`${ActiveFilter === 1 ? "Active" : ""}`} onClick={() => SetActiveFilter(1)}>All Members</li>
                        <li className={`${ActiveFilter === 2 ? "Active" : ""}`} onClick={() => SetActiveFilter(2)}>My Groups</li>
                        <li className={`${ActiveFilter === 3 ? "Active" : ""}`} onClick={() => SetActiveFilter(3)}>My Favorites</li>
                        <li className={`mentions ${ActiveFilter === 4 ? "Active" : ""}`} onClick={() => SetActiveFilter(4)}>Mentions</li>
                    </ul>
                </section>

                <section className='flex flex-col gap-5'>
                    <Post></Post>
                    <Post></Post>
                    <Post></Post>
                </section>

            </div>


            {/*Avtive and group part*/}
            <div className='active-group w-[25%] flex flex-col gap-5 sticky top-0'>
                <Active></Active>
                <Group></Group>
            </div>

        </div>
    )
}
