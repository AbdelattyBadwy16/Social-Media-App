import React, { useState } from 'react'
import "./Messenger.css"

export default function BottomMessenger() {

    const [ActiveMessenger, setActiveMessenger] = useState(false);

    return (
        <div className={`BottomMessenger fixed right-10  z-[1000]  bg-[white] bottom-0 w-[300px] transition-all border shadow-lg ${ActiveMessenger ? "h-[500px]" : "h-[60px]"}`}>

            <section onClick={() => setActiveMessenger(!ActiveMessenger)} className="flex items-center justify-between  cursor-pointer w-[100%] p-5">
                <div className="flex gap-3 items-center">
                    <img src='/image/profile.jpg' width={30} className='rounded-full cursor-pointer'></img>
                    <p className="text-black font-bold">Messenger</p>
                </div>

                <p className={`text-[25px] ${ActiveMessenger ? "rotate-180" : "v"}`}>^</p>

            </section>

            <section className="h-[90%] bg-[#f9f9f9]">
                <div className="p-3 w-[100%]">
                    <input type="text" className="w-[100%] p-2 bg-[#f9f9f9] border-2 border-gray-300 rounded-full border-solid" placeholder="Search Member..."></input>
                </div>
                <div>

                    <div className="MessengerCard flex gap-3 border-t-2 border-b-2 border-gray-300 p-3 cursor-pointer">
                        <img src='/image/profile.jpg' width={50} className='rounded-full cursor-pointer'></img>
                        <div>
                            <h3 className="font-bold">Abdo</h3>
                            <p className="text-gray-600 text-[13px]">You : taat</p>
                        </div>
                    </div>

                    <div className="MessengerCard flex gap-3 border-t-2 border-b-2 border-gray-300 p-3 cursor-pointer">
                        <img src='/image/profile.jpg' width={50} className='rounded-full cursor-pointer'></img>
                        <div>
                            <h3 className="font-bold">Abdo</h3>
                            <p className="text-gray-600 text-[13px]">You : taat</p>
                        </div>
                    </div>

                    <div className=" MessengerCard flex gap-3 border-t-2 border-b-2 border-gray-300 p-3 cursor-pointer">
                        <img src='/image/profile.jpg' width={50} className='rounded-full cursor-pointer'></img>
                        <div>
                            <h3 className="font-bold">Abdo</h3>
                            <p className="text-gray-600 text-[13px]">You : taat</p>
                        </div>
                    </div>

                </div>
            </section>
        </div >
    )
}
