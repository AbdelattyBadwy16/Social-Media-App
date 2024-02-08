import { GetFollower, GetFriends } from '../../Helper/AccountApi';
import React, { useEffect, useState } from 'react'
import FriendCard from './FriendCard';
import Spinner from '../Spinner';

export default function Follower() {
    const [isLoading, setIsLoding] = useState(false);
    const [Followers, setFollowers] = useState([]);

    useEffect(() => {
        async function fetch() {
            setIsLoding(true);
            try {
                const data = await GetFollower();
                setFollowers(data);
                console.log(data);
            } catch {

            } finally {
                setIsLoding(false);
            }

            return;
        };
        fetch();
    }, [])

    const type = "Follower";
    return (
        <div className='m-10 bg-[white] p-5 rounded-lg border-2'>
            <h3 className='mb-3 font-bold text-gray-500 text-[20px]'>Followers</h3>
            <hr></hr>
            {
                isLoading ? <Spinner></Spinner> :
                    <>
                        {
                            Followers.length ?
                                <div className='grid md:grid-cols-4 mt-10 gap-5 h-auto sm:grid-cols-2 grid-cols-1'>
                                    {
                                        Followers.map((friend) =>
                                            <FriendCard type={type} key={friend.id} user={friend}></FriendCard>)
                                    }
                                </div> : <p className='text-center mt-10 text-[30px] font-bold'>No Followers have yet.</p>
                        }
                    </>

            }
        </div >
    )
}
