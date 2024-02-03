import React, { useState } from 'react'
import "./Profile.css"
import { Link, Outlet } from 'react-router-dom';

export default function Profile() {
  const [ActiveItem, setActiveItem] = useState(1);
  return (
    <div className="container">
      <section className="bg-[white] border-2 border-gray-200 rounded-lg">
        <img className="w-[100%] h-[300px]" src="/image/loginBack.jpg" />
        <div className="ProfileText p-5 flex items-center gap-5">
          <img src="/image/Abdo.jpg" className="rounded-lg" width={130}></img>
          <div >
            <div className="flex gap-3 items-center">
              <h1 className="font-bold text-[25px]">Abdelatty</h1>
              <h3 className="text-[11px] font-bold border-2 border-gray-200 p-1 rounded-md  text-gray-600">Community Head</h3>
            </div>
            <div className="flex gap-3 mb-5">
              <p>@admin</p>
              <p>Joined : March 5,2024</p>
            </div>
            <div className="flex gap-3">
              <div className="flex gap-2 items-center">
                <p className="font-bold text-[20px]">12</p>
                <p>Followers</p>
              </div>
              <div className="flex gap-2 items-center">
                <p className="font-bold text-[20px]">5</p>
                <p>Following</p>
              </div>
            </div>
          </div>
        </div>
        <hr ></hr>
        <div className="ProfileList m-5">
          <ul className="flex gap-5 items-center justify-center">
            <Link to="activity"><li onClick={() => setActiveItem(1)} className={`${ActiveItem === 1 ? "ActiveProfileItem" : ""} `}>Activity</li></Link>
            <Link to="Friends"><li onClick={() => setActiveItem(2)} className={`${ActiveItem === 2 ? "ActiveProfileItem" : ""} `}>Friends</li></Link>
            <li onClick={() => setActiveItem(3)} className={`${ActiveItem === 3 ? "ActiveProfileItem" : ""} `}>Photos</li>
            <li onClick={() => setActiveItem(4)} className={`${ActiveItem === 4 ? "ActiveProfileItem" : ""} del`}>Messeges</li>
            <Link to="Groups"><li onClick={() => setActiveItem(5)} className={`${ActiveItem === 5 ? "ActiveProfileItem" : ""} del`}>Groups</li></Link>
            <li onClick={() => setActiveItem(6)} className={`${ActiveItem === 6 ? "ActiveProfileItem" : ""} del`}>About</li>
            <li onClick={() => setActiveItem(7)} className={`${ActiveItem === 7 ? "ActiveProfileItem" : ""} `}>Setting</li>
          </ul>
        </div>
      </section>

      <Outlet></Outlet>

    </div>
  )
}
