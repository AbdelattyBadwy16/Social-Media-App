import React, { useState } from 'react'
import './Photos.css'

export default function Photos() {
  const [ActiveFilter, setActivefilter] = useState(1);

  return (
    <div className='container'>
      <h3 className="font-semibold text-[30px] text-gray-700">Photos</h3>
      <hr></hr>

      <section className="border-b-1 flex justify-between items-cente p-4 rounded-md mt-10">
        <ul className="flex gap-5 text-gray-500 font-bold text-[12px]">
          <li className={`${ActiveFilter === 1 ? "Active" : ""} relative cursor-pointer flex gap-2 items-center`} onClick={() => setActivefilter(1)}>
            <div>All Photos</div>
            <div className="cirle w-[15px] h-[15px] rounded-full bg-gray-500 items-center justify-center text-[10px] text-center bottom-5 text-[white]">2</div>
          </li>
          <li className={`${ActiveFilter === 2 ? "Active" : ""} relative cursor-pointer flex gap-2 items-center`} onClick={() => setActivefilter(2)}>
            <div>My Photos</div>
            <div className="cirle w-[15px] h-[15px] rounded-full bg-gray-500 items-center justify-center text-[10px] text-center bottom-5 text-[white]">0</div>
          </li>
        </ul>
        <div className="bg-[white] shadow-md p-2 rounded-lg flex gap-3 ">
          <img className='cursor-pointer' src="/icon/search.png" width={20}></img>
          <input type='text' className='search  w-[90%]  ' placeholder='Search Photos'></input>
        </div>
      </section>


      <section className="grid gap-5 grid-cols-1 sm:grid-cols-2  md:grid-cols-3 lg:grid-cols-4 mt-10 mr-5">
        <img src="/image/loginBack.jpg" className="rounded-lg cursor-pointer"></img>
        <img src="/image/loginBack.jpg" className="rounded-lg cursor-pointer"></img>
        <img src="/image/loginBack.jpg" className="rounded-lg cursor-pointer"></img>
        <img src="/image/loginBack.jpg" className="rounded-lg cursor-pointer"></img>
        <img src="/image/loginBack.jpg" className="rounded-lg cursor-pointer"></img>
      </section>


    </div>
  )
}
