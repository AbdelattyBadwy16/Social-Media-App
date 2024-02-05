import React from 'react'

interface Data{
  about : string
}

export default function About(data: Data) {
  return (
    <div className="bg-[white] border-2 w-[100%] h-[200px] rounded-md p-5 overflow-scroll">
      <h3 className="text-gray-500 font-bold  ">About</h3>
      <p className="text-[15px] mt-5">
        {data.about != null ? data.about : "I,m a new User in Glichat App."}
      </p>
    </div>
  )
}
