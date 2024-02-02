import React from 'react'

export default function Group() {
    return (
        <div className=' bg-[white] border shadow-lg p-3 cursor-pointer rounded-lg sticky top-0 '>
            <p className='text-gray-500 text-[18px]'>Groups</p>
            <section className='mt-5 flex flex-col gap-5'>

                <div className='relative flex gap-5 w-[100% items-center'>
                    <img src="/image/loginBack.jpg" className='rounded-full' width={50}></img>
                    <div>
                        <h3 className='font-bold'>Graphic Design</h3>
                        <p className='text-gray-500 text-[12px]'>14 members</p>
                    </div>
                </div>

                <div className='relative flex gap-5 w-[100% items-center'>
                    <img src="/image/loginBack.jpg" className='rounded-full' width={50}></img>
                    <div>
                        <h3 className='font-bold'>Graphic Design</h3>
                        <p className='text-gray-500 text-[12px]'>14 members</p>
                    </div>
                </div>

                <div className='relative flex gap-5 w-[100% items-center'>
                    <img src="/image/loginBack.jpg" className='rounded-full' width={50}></img>
                    <div>
                        <h3 className='font-bold'>Graphic Design</h3>
                        <p className='text-gray-500 text-[12px]'>14 members</p>
                    </div>
                </div>

            </section>
        </div>
    )
}
