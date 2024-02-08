import { GetUserData } from '../../Helper/ProfileApi';
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import Cookies from 'universal-cookie';
import Spinner from '../Spinner';
export interface friend {
    id: string,
    FollowerId: string,
    type : string
}
export default function FriendCard(user: friend) {
    const cookie = new Cookies();
    const id = cookie.get("id");
    const [isFreind, setIsFreind] = useState(false);
    const [isLoading, setIsLoding] = useState(false);
    const [data, setData] = useState({});

    // fetch user data
    useEffect(() => {
        setIsLoding(true);

        async function fetch() {
            try {
                let data;
                if (user.type!="Follower")
                    data = await GetUserData(user.user.followerId);
                else data = await GetUserData(user.user.userId);
                setData(data);
                console.log(user.type)
            } finally {
                setIsLoding(false);
            }
        }
        fetch();
    }, []);



    return (

        <div className="bg-[white] rounded-lg border w-auto" >
            <img src="/image/loginBack.jpg" className="w-[100%] h-[200px]"></img>
            {
                isLoading ? <Spinner></Spinner> :
                    <section className="p-5 flex flex-col gap-5 text-center items-center">
                        <img src={`https://localhost:7279//userIcon/${data.iconImagePath}`} className="w-[40%] h-[80px] relative bottom-[50px] rounded-xl shadow-md"></img>
                        <div className="flex flex-col gap-2 relative bottom-10">
                            <h2 className="font-bold">{data.firstName + " " + data.lastName}</h2>
                            <p className="text-[10px] text-gray-500">user</p>
                        </div>
                        <div className="flex justify-around gap-10">
                            <div>
                                <h3 className="font-bold">{data.followers}</h3>
                                <p className="text-gray-500 text-[12px]">Followers</p>
                            </div>
                            <div>
                                <h3 className="font-bold">{data.following}</h3>
                                <p className="text-gray-500 text-[12px]">Following</p>
                            </div>
                        </div>
                        <div className="flex items-center justify-around gap-10">
                            <button className="bg-gray-300 p-3 w-[100%] rounded-md border shadow-md ">Show Profile</button>
                        </div>
                    </section>
            }
        </div >

    )
}
