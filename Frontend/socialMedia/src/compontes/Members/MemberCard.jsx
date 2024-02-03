import React from 'react'

export default function MemberCard() {
    return (
        <div className="bg-[white] rounded-lg border w-auto">
            <img src="/image/loginBack.jpg" className="w-[100%] h-[200px]"></img>
            <section className="p-5 flex flex-col gap-5 text-center items-center">
                <img src="/image/loginBack.jpg" className="w-[40%] h-[80px] relative bottom-[50px] rounded-xl shadow-md"></img>
                <div className="flex flex-col gap-2 relative bottom-10">
                    <h2 className="font-bold">Abdelatty</h2>
                    <p className="text-[10px] text-gray-500">Community Head</p>
                </div>
                <div className="flex justify-around gap-10">
                    <div>
                        <h3 className="font-bold">12</h3>
                        <p className="text-gray-500 text-[12px]">Followers</p>
                    </div>
                    <div>
                        <h3 className="font-bold">5</h3>
                        <p className="text-gray-500 text-[12px]">Following</p>
                    </div>
                </div>
                <div className="flex items-center justify-around gap-10">
                    <button className="bg-gray-300 p-3 w-[70%] rounded-md border shadow-md ">+ Follow</button>
                    <button className="bg-gray-300 border shadow-md p-2 rounded-lg"><img src="/icon/message.png" width={30}></img></button>
                </div>
            </section>
        </div>
    )
}
