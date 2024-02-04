import React from 'react'

export default function GroupCard() {
    return (
        <div className="bg-[white] shadow-lg border rounded-lg">
            <img src="/image/loginBack.jpg" className="w-[100%] h-[200px]"></img>
            <section className="p-5 flex flex-col gap-5 rounded-lg">
                <img src="/image/loginBack.jpg" className="w-[40%]  h-[80px] relative bottom-[50px] rounded-lg shadow-md"></img>
                <div className="flex flex-col gap-2">
                    <h2 className="font-bold">Archticture</h2>
                    <p className="text-[10px] text-gray-500">Public Group / 2 members</p>
                </div>
                <div className="flex items-center justify-between">
                    <section className="flex items-center">
                        <img src='/image/profile.jpg' width={20} className='rounded-full cursor-pointer'></img>
                        <img src='/image/profile.jpg' width={20} className='rounded-full cursor-pointer relative right-2'></img>
                        <img src='/image/profile.jpg' width={20} className='rounded-full cursor-pointer relative right-4'></img>
                        <p className="text-gray-500 text-[12px]">+9</p>
                    </section>
                    <button className="Cardbtn bg-blue-500 text-white p-3 rounded-lg w-[30%] ">Join</button>
                </div>
            </section>
        </div>
    )
}
