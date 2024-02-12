import React, { useContext, useEffect, useState } from 'react'
import "./Profile.css"
import { Link, Outlet } from 'react-router-dom';
import Cookies from 'universal-cookie';
import { GetUserData, UpdateIconImage } from '../Helper/ProfileApi';
import Spinner from '../compontes/Spinner';
import { postWindow } from '../Context/PostWindow';
import { toast } from 'react-toastify';


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
  createdDate: string,
  iconImagePath: string,
  jopTitle: string
}

export default function Profile() {
  const [ActiveItem, setActiveItem] = useState(1);
  const [editImage, setEditImage] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [data, setData] = useState<userData>({ jopTitle: "", iconImagePath: "", createdDate: "", userName: "", firstName: "", secondName: "", about: "", bitrhDate: "", email: "", country: "", phoneNumber: "", followers: 0, following: 0 });
  const cookie = new Cookies();
  // fetch user data
  useEffect(() => {
    setIsLoading(true);

    async function fetch() {
      try {
        const id = cookie.get("id");
        const data = await GetUserData(id);
        setData(data);
      } finally {
        setIsLoading(false);
      }
    }
    fetch();
  }, []);

  // handel edit Iconimage
  async function handelIconImage() {
    var f = document.getElementById("form");
    const cookie = new Cookies();
    const newForm = new FormData(f);
    const res = await UpdateIconImage(newForm);
    if (res.ok) {
      toast("Photo Change Correctly.");
      setTimeout(() => {
        async function fetch() {
          try {
            const id = cookie.get("id");
            const data = await GetUserData(id);
            setData(data);
            cookie.remove("image");
            cookie.set("image", data.iconImagePath);
          } finally {
            setIsLoading(false);
          }
        }
        fetch();
      }, 2000);

    }
    return;
  }


  const PostWindow = useContext(postWindow);
  const PostCheck = cookie.get("PostWindow");

  const CreatedDate = new Date(data?.createdDate);
  var mL = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  return (
    <>
      <div className="container mb-[50px]">
        {isLoading ? <Spinner></Spinner> :
          <>
            <section className="bg-[white] border-2 border-gray-200 rounded-lg">
              <div>
                <img className="w-[100%] h-[300px]" src="/image/loginBack.jpg" />
              </div>
              <div className="ProfileText p-5 flex items-center gap-5">
                <div className='relative'>
                  <div className='relative'>
                    <img onClick={() => setEditImage(!editImage)} src={`https://localhost:7279//userIcon/${data.iconImagePath}`} className="rounded-lg hover:opacity-40 cursor-pointer" width={130}></img>
                  </div>
                  <form onSubmit={handelIconImage} id="form">
                    <input name="image" onChange={handelIconImage} id='file' type='file' className={`mt-3 ${editImage ? "" : "hidden"} w-[100px]`}></input>
                  </form>
                </div>
                <div >
                  <div className="flex gap-3 items-center">
                    <h1 className="font-bold text-[25px]">{data.firstName}</h1>
                    { 
                      data.nickName != null ?
                      <h3>({data.nickName})</h3> : ""
                    }
                    <h3 className="text-[11px] font-bold border-2 border-gray-200 p-1 rounded-md  text-gray-600">{data.jopTitle}</h3>
                  </div>
                  <div className="flex gap-3 mb-5">
                    <p>@{data.userName}</p>
                    <p>Joined : {mL[Number(CreatedDate.getMonth() + 1)]} {CreatedDate.getDay()},{CreatedDate.getFullYear()}</p>
                  </div>
                  <div className="flex gap-3">
                    <Link to="Follower" className="flex gap-2 items-center">
                      <p className="font-bold text-[20px]">{data.followers}</p>
                      <p>Followers</p>
                    </Link>
                    <Link to="Following" className="flex gap-2 items-center">
                      <p className="font-bold text-[20px]">{data.following}</p>
                      <p>Following</p>
                    </Link>
                  </div>
                </div>
              </div>
              <hr ></hr>
              <div className="ProfileList m-5">
                <ul className="flex gap-5 items-center justify-center">
                  <Link to="activity"><li onClick={() => setActiveItem(1)} className={`${ActiveItem === 1 ? "ActiveProfileItem" : ""} `}>Activity</li></Link>
                  <Link to="Photos"><li onClick={() => setActiveItem(3)} className={`${ActiveItem === 3 ? "ActiveProfileItem" : ""} `}>Photos</li></Link>
                  <li onClick={() => setActiveItem(4)} className={`${ActiveItem === 4 ? "ActiveProfileItem" : ""} `}>Messeges</li>
                  <Link to="Groups"><li onClick={() => setActiveItem(5)} className={`${ActiveItem === 5 ? "ActiveProfileItem" : ""} del`}>Groups</li></Link>
                  <Link to="Setting"><li onClick={() => setActiveItem(7)} className={`${ActiveItem === 7 ? "ActiveProfileItem" : ""} `}>Setting</li></Link>
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
