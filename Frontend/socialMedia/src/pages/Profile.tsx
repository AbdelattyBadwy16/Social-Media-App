import React, { useContext, useEffect, useState } from 'react'
import "./Profile.css"
import { Link, Outlet } from 'react-router-dom';
import Cookies from 'universal-cookie';
import { GetUserData } from '../Helper/ProfileApi';
import Spinner from '../compontes/Spinner';
import { postWindow } from '../Context/PostWindow';


interface userData {
  firstName: string,
  secondName: string,
  about: string,
  bitrhDate: string,
  email: string,
  country: string,
  phoneNumber: string,
  followers: number,
  following: number,
  userName: string,
  createdDate: string
}

export default function Profile() {
  const [ActiveItem, setActiveItem] = useState(1);
  const [isLoading, setIsLoading] = useState(false);
  const [addNewPost, setAddNewPost] = useState(false);
  const [data, setData] = useState<userData>({ createdDate: "", userName: "", firstName: "", secondName: "", about: "", bitrhDate: "", email: "", country: "", phoneNumber: "", followers: 0, following: 0 });
  const cookie = new Cookies();
  // fetch user data
  useEffect(() => {
    setIsLoading(true);
    const token = cookie.get("bearer");
    const username = cookie.get("userName");
    async function fetch() {
      try {
        const data = await GetUserData();
        setData(data);
      } finally {
        setIsLoading(false);
      }
    }
    fetch();
  }, []);
  const PostWindow = useContext(postWindow);
  const PostCheck = cookie.get("PostWindow");

  const CreatedDate = new Date(data?.createdDate);
  var mL = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  return (
    <>
      <div className="container">
        {isLoading ? <Spinner></Spinner> :
          <>
            <section className="bg-[white] border-2 border-gray-200 rounded-lg">
              <img className="w-[100%] h-[300px]" src="/image/loginBack.jpg" />
              <div className="ProfileText p-5 flex items-center gap-5">
                <img src="/image/Abdo.jpg" className="rounded-lg" width={130}></img>
                <div >
                  <div className="flex gap-3 items-center">
                    <h1 className="font-bold text-[25px]">{data.firstName}</h1>
                    <h3 className="text-[11px] font-bold border-2 border-gray-200 p-1 rounded-md  text-gray-600">Community Head</h3>
                  </div>
                  <div className="flex gap-3 mb-5">
                    <p>@{data.userName}</p>
                    <p>Joined : {mL[Number(CreatedDate.getMonth()+1)]} {CreatedDate.getDay()},{CreatedDate.getFullYear()}</p>
                  </div>
                  <div className="flex gap-3">
                    <div className="flex gap-2 items-center">
                      <p className="font-bold text-[20px]">{data.followers}</p>
                      <p>Followers</p>
                    </div>
                    <div className="flex gap-2 items-center">
                      <p className="font-bold text-[20px]">{data.following}</p>
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
          </>
        }
      </div>

      {
        PostCheck ?
          <>
            <div className='w-[100%] h-[5000px] z-[1000] top-0 bg-gray-500 opacity-80 rounded-lg absolute flex items-center justify-center'>
            </div>
          </> : ""
      }

    </>
  )
}
