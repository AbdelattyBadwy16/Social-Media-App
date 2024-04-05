import { GetUserData } from '../../Helper/ProfileApi';
import { GetFollower } from '../../Helper/AccountApi';
import React, { useEffect, useState } from 'react'



export default function Active() {
    const [active, setActive] = useState([]);
    useEffect(() => {
        async function fetch() {
            const data = await GetFollower();
            for(let i=0 ; i<data.length ; i++){
                const freind : any = await GetUserData(data[i].userId);
                setActive([...active , freind]);
            }
        }
        fetch();
    }, []);
    return (
        <div className='bg-[white] items-center justify-between border shadow-lg p-3 rounded-lg'>
            <p className='text-gray-500 text-[13px]'>RECENTLY ACTIVE MEMBERS</p>
            <section className='mt-5 grid grid-cols-4 gap-3'>
                {
                    active.map((item)=><div className='relative'>
                    <img src={`https://localhost:7279//userIcon/${item.iconImagePath}`} className="rounded-full" width={40}></img>
                </div>)
                    
                }
            </section>
        </div>
    )
}
