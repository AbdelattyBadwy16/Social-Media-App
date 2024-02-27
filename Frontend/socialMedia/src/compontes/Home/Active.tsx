import React, { useEffect, useState } from 'react'



export default function Active() {
    const [dist , setDist] = useState([]);
    useEffect(()=>{
        
    },[]);

    return (
        <div className='bg-[white] items-center justify-between border shadow-lg p-3 rounded-lg'>
            <p className='text-gray-500 text-[13px]'>RECENTLY ACTIVE MEMBERS</p>
            <section className='mt-5 grid grid-cols-4 gap-3'>

                <div className='relative'>
                    <img src="/image/profile.jpg" width={40}></img>
                    <div className='green-point w-[15px] h-[15px] absolute bg-green-900 rounded-full right-5 bottom-0'></div>
                </div>
              
            </section>
        </div>
    )
}
