import React from 'react'

export default function Blog() {
    return (
        <div className='w-[25%] bg-[white] shadow-md border p-5 rounded-md sticky top-0'>
            <p className='text-gray-500 text-[20px]'>BLOG</p>
            <section>
                <div className='flex gap-3 p-5 cursor-pointer'>
                    <img src="/image/loginBack.jpg" className='rounded-lg' width={50}></img>
                    <div>
                        <p className='text-gray-500 text-[10px]'>March 18,2024</p>
                        <h1>What is an Online Community?</h1>
                    </div>
                </div>
                <div className='flex gap-3 p-5 cursor-pointer'>
                    <img src="/image/loginBack.jpg" className='rounded-lg' width={50}></img>
                    <div>
                        <p className='text-gray-500 text-[10px]'>March 18,2024</p>
                        <h1>What is an Online Community?</h1>
                    </div>
                </div>
                <div className='flex gap-3 p-5 cursor-pointer'>
                    <img src="/image/loginBack.jpg" className='rounded-lg' width={50}></img>
                    <div>
                        <p className='text-gray-500 text-[10px]'>March 18,2024</p>
                        <h1>What is an Online Community?</h1>
                    </div>
                </div>
            </section>
        </div>
    )
}
