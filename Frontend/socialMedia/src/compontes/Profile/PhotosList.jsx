import React from 'react'

export default function PhotosList() {
    return (
        <div className="bg-[white] border-2 w-[100%] h-[200px] rounded-md p-5 mt-5">
            <h3 className="text-gray-500 font-bold">Photos</h3>
            <div className="text-[15px] mt-5">
                <img src="/image/loginBack.jpg" className="rounded-lg" width={80}></img>
            </div>
        </div>
    )
}
