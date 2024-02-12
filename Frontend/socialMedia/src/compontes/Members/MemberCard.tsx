import { AddFriends, CheckFriend, RemoveFriend } from '../../Helper/AccountApi';
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import Cookies from 'universal-cookie';
export interface account {
    id: string,
    UserName: string,
    FirstName: string,
    LastName: string,
    Password: string,
    Email: string,
    Gender: string,
    PhoneNumber: string,
    Country: string,
    iconImagePath: string

}
export default function MemberCard(user: account) {
    user = user.user;
    const cookie = new Cookies();
    const id = cookie.get("id");
    const [isFreind, setIsFreind] = useState(false);


    // check if Friend of not
    useEffect(() => {
        async function fetch() {
            const res = await CheckFriend(user.id);
            if(res===true)setIsFreind(true);
        }
        fetch();
    }, []);


    // remove Follow
    async function handelRemoveFollow() {
        setIsFreind(false);
        const res = RemoveFriend(user.id);
        return;
    }

    // add Follow
    async function handelAddFollow() {
        const res = await AddFriends(user.id);
        setIsFreind(true);
        return;
    }

    return (
        <div className="bg-[white] rounded-lg border w-auto">
            <img src="/image/loginBack.jpg" className="w-[100%] h-[200px]"></img>
            <section className="p-5 flex flex-col gap-5 text-center items-center">
                <img src={`https://localhost:7279//userIcon/${user.iconImagePath}`} className="w-[40%] h-[80px] relative bottom-[50px] rounded-xl shadow-md"></img>
                <div className="flex flex-col gap-2 relative bottom-10">
                    <h2 className="font-bold">{user.firstName + " " + user.lastName}</h2>
                    <p className="text-[10px] text-gray-500">user</p>
                </div>
                <div className="flex justify-around gap-10">
                    <div>
                        <h3 className="font-bold">{user.followers}</h3>
                        <p className="text-gray-500 text-[12px]">Followers</p>
                    </div>
                    <div>
                        <h3 className="font-bold">{user.following}</h3>
                        <p className="text-gray-500 text-[12px]">Following</p>
                    </div>
                </div>
                <div className="flex items-center justify-around gap-10">
                    {
                        id != user.id ?
                            <>
                                {isFreind ? <button onClick={handelRemoveFollow} className="bg-[white] p-3 w-[70%] rounded-md border shadow-md ">unfollow</button>
                                    :
                                    <button onClick={handelAddFollow} className="bg-gray-300 p-3 w-[70%] rounded-md border shadow-md ">+ Follow</button>
                                }
                                <button className="bg-gray-300 border shadow-md p-2 rounded-lg"><img src="/icon/message.png" width={30}></img></button>
                            </> : <Link to="/profile" className="bg-gray-600 text-white p-3 w-[100%] rounded-md border shadow-md ">Show My Profile</Link>
                    }
                </div>
            </section>
        </div>
    )
}
