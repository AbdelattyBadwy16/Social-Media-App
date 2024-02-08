import React, { useEffect, useState } from 'react'
import Active from '../compontes/Home/Active';
import MemberCard from '../compontes/Members/MemberCard';
import './Members.css'
import { GetAllUsers } from '../Helper/AccountApi';

export default function Members() {
  const [ActiveFilter, setActivefilter] = useState(1);
  const [users,setUsers] = useState([]);
  
  useEffect(()=>{
      async function  fetch() {
          const res = await GetAllUsers();
          setUsers(res);
      }
      fetch();
  },[]);

  return (
    <div className='container flex items-start justify-between'>

      <section className="Gruops w-[70%]">

        <div className="border-b-1 flex justify-between items-center border shadow-lg bg-[white] p-4 rounded-md">
          <ul className="flex gap-5 text-gray-500 font-bold text-[12px]">
            <li className={`${ActiveFilter === 1 ? "Active" : ""} relative cursor-pointer`} onClick={() => setActivefilter(1)}>
              <div className="cirle w-[15px] h-[15px] rounded-full bg-gray-500 items-center justify-center text-[10px] text-center absolute right-0 bottom-5 text-[white]">2</div>
              <div>All Members</div>
            </li>
          </ul>
          <div className="searchMember w-[10%]shadow-md p-2 rounded-lg flex gap-3 ">
            <img className='cursor-pointer' src="/icon/search.png" width={20}></img>
            <input type='text' className='  w-[90%]  ' placeholder='Search Members'></input>
          </div>
        </div>

        <div className="grid gap-5 grid-cols-1 sm:grid-cols-2  md:grid-cols-3 mr-5 m-5">
          {
            users.map((item)=><MemberCard user={item}></MemberCard>)
          }
          
        </div>
      </section>

      <section className="ActiveList w-[25%] sticky top-0">
        <Active></Active>
      </section>

    </div>
  )
}
