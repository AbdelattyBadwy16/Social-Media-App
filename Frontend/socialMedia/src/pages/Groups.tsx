import React, { useState } from 'react'
import GroupCard from '../compontes/Groups/GroupCard';
import './Groups.css'


export default function Groups() {
  let x = 5;
  
  const [ActiveFilter, setActivefilter] = useState(1);

  return (
    <div className='container flex flex-col gap-10 rounded-lg'>

      <section className="border-b-1 flex justify-between items-center border shadow-lg bg-[white] p-4 rounded-md">
        <ul className="flex gap-5 text-gray-500 font-bold text-[12px]">
          <li className={`${ActiveFilter === 1 ? "Active" : ""} relative cursor-pointer`} onClick={() => setActivefilter(1)}>
            <div className="cirle w-[15px] h-[15px] rounded-full bg-gray-500 items-center justify-center text-[10px] text-center absolute right-0 bottom-5 text-[white]">2</div>
            <div>All Groups</div>
          </li>
          <li className={`${ActiveFilter === 2 ? "Active" : ""} relative cursor-pointer`} onClick={() => setActivefilter(2)}>
            <div className="cirle w-[15px] h-[15px] rounded-full bg-gray-500 items-center justify-center text-[10px] text-center absolute right-0 bottom-5 text-[white]">0</div>
            <div>My Groups</div>
          </li>
        </ul>
        <div className="w-[10%]shadow-md p-2 rounded-lg flex gap-3 ">
          <img className='cursor-pointer' src="/icon/search.png" width={20}></img>
          <input type='text' className='search  w-[90%]  ' placeholder='Search Groups'></input>
        </div>
      </section>

      <section className="GroupCards grid gap-5 grid-cols-1 sm:grid-cols-2  md:grid-cols-3 lg:grid-cols-4 mr-5">
        <GroupCard></GroupCard>
        <GroupCard></GroupCard>
        <GroupCard></GroupCard>
        <GroupCard></GroupCard>
        <GroupCard></GroupCard>
      </section>
    </div>
  )
}
