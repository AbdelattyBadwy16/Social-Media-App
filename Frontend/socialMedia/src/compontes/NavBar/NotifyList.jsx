import React from 'react'
import { Link } from 'react-router-dom'
import './List.css'

export default function NotifyList() {
    return (
        <div className='w-[300px] h-[500px] shadow-lg  bg-[white] border z-[1000] absolute right-4 top-20'>
            <section className='flex justify-around items-center bg-gray-200 p-2 border shadow-md'>
                <p className='text-[15px] font-semibold '>NOTIFICATION </p>
                <Link className='text-[12px] text-blue-600'>VIEW ALL</Link>
            </section>

            <section className=' rounded-lg flex gap-5 flex-col'>
                <div className='Notfiy flex p-5 gap-5'>
                    <img src='/image/profile.jpg' width={30} className='rounded-full'></img>
                    <div>
                        <h5>Abdelatty</h5>
                        <p className='text-[10px] font-bold text-gray-900'>35 min age</p>
                    </div>
                </div>
                <div className='Notfiy p-5 flex gap-5'>
                    <img src='/image/profile.jpg' width={30} className='rounded-full'></img>
                    <div>
                        <h5>Abdelatty</h5>
                        <p className='text-[10px] font-bold text-gray-900'>35 min age</p>
                    </div>
                </div>
            </section>

        </div>
    )
}
