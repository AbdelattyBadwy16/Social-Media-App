import React from 'react'
import Post from '../Home/Post'
import Active from '../Home/Active'
import About from './About'
import PhotosList from './PhotosList'
import "./Activity.css"

export default function Activity() {
    return (
        <div className=' m-auto  mt-10 flex gap-10 items-start justify-center relative w-[100%]' >
            <section className="active sticky top-0">
                <Active></Active>
            </section>

            {/*post part*/}
            <div className='posts w-[50%] flex flex-col gap-5' >

                <section className='flex bg-[white] items-center justify-between border shadow-lg p-3 cursor-pointer rounded-lg'>
                    <section className='flex items-center gap-5'>
                        <img src="/image/Abdo.jpg" className="rounded-full" width={30}></img>
                        <h3 className='text-gray-500'>What's on your mind?</h3>
                    </section>
                    <section className='bg-gray-300 rounded-full p-1 border shadow-lg'>
                        <img src="/icon/MyPhoto.png" width={20} />
                    </section>
                </section>



                <section className='flex flex-col gap-5'>
                    <Post></Post>
                    <Post></Post>
                    <Post></Post>
                </section>

            </div >

            <section className="about-photo sticky top-0 w-[25%]">
                <About></About>
                <PhotosList></PhotosList>
            </section>


        </div >
    )
}
