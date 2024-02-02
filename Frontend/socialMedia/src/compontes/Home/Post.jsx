import React from 'react'

export default function Post() {
    return (
        <div className='bg-[white] border shadow-lg rounded-lg'>

            <div className='flex items-start justify-between  p-5'>
                <div className='flex gap-5'>
                    <img src="/image/profile.jpg" width={30}></img>
                    <div>
                        <p> <span className='font-bold'>Abdelatty</span> posted an update</p>
                        <p className='text-[12px] text-gray-500'>3 hours ago </p>
                    </div>
                </div>
                <div className='text-[30px]'>...</div>
            </div>

            <div className='mt-10 p-5'>
                <p className='mb-5'>este es un tema muy interesante</p>
                <hr></hr>
                <div className='flex gap-5 mt-2  justify-between'>
                    <div className='flex'>
                        <div className='flex gap-2 items-center'>
                            <img src="/icon/inbox.png" width={20}></img>
                            <p>Like</p>
                        </div>
                        <div className='flex gap-2 items-center'>
                            <img src="/icon/notif.png" width={20}></img>
                            <p>Comment</p>
                        </div>
                    </div>
                    <div className='flex gap-2'>
                        <img src="/icon/notif.png" width={20}></img>
                        <p>share</p>
                    </div>
                </div>
            </div>

            <div className='bg-[#f2f2f2] p-5 w-[100%]'>
                <div className='flex gap-5 w-[100%]'>
                    <img src="/image/profile.jpg" className='rounded-full' width={30}></img>
                    <div className="w-[40%] shadow-md p-2 rounded-lg flex bg-[white] ">
                        <input type='text' className='search  w-[100%]' placeholder='Type a comment...'></input>
                        <img className='cursor-pointer' src="/icon/inbox.png" width={20}></img>
                    </div>
                </div>
            </div>

        </div>
    )
}
