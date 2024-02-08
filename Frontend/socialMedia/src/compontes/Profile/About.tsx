import Cookies from 'universal-cookie';
import { GetUserData, UpdateAbout } from '../../Helper/ProfileApi';
import React, { useContext, useEffect, useState } from 'react'

interface Data {
  about: string
}

export default function About() {
  const [openList, setOpenList] = useState(false);
  const [openEdit, setOpenEdit] = useState(false);
  const [about, setAbout] = useState("");

  useEffect(() => {
    async function fetch() {
      const cookie = new Cookies();
      const id = cookie.get("id");
      const data = await GetUserData(id);
      setAbout(data.about);
    }
    fetch();
    
  }, [])

  // edit about
  async function handelUpdate() {
    setOpenEdit(false);

    const res = await UpdateAbout(about);
    setAbout(res.about);
    return;
  }
  return (
    <div className="bg-[white] border-2 w-[100%] h-[200px] rounded-md p-5 overflow-scroll">
      <div className='flex justify-between items-center'>
        <h3 className="text-gray-500 font-bold">About</h3>
        <p onClick={() => setOpenList(!openList)} className='text-[30px] font-bold cursor-pointer'>...</p>
        {
          openList ?
            <div onClick={() => { setOpenList(false); setOpenEdit(true); setAbout(data.about) }} className='absolute bg-gray-300 shadow-lg p-2 rounded-lg hover:bg-gray-500 cursor-pointer right-8 top-[60px]'>
              Edit
            </div> : ""
        }
      </div>
      {
        openEdit ?
          <>
            <textarea className='w-[100%] h-[50%]' value={about} onChange={(e) => setAbout(e.target.value)} ></textarea>
            <button onClick={handelUpdate} className='bg-gray-300 shadow-lg p-2 rounded-md hover:bg-gray-500'>Save</button>
          </>
          :
          <p className="text-[15px] mt-5">
            {about}
          </p>
      }
    </div>
  )
}
