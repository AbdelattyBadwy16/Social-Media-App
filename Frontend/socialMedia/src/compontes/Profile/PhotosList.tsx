import React, { useEffect, useState } from 'react'
import Spinner from '../Spinner';
import { GetUserPhotos, GetUserPhotosTop3 } from '../../Helper/PhotoApi';

export default function PhotosList() {
    const [isLoading, setIsLoding] = useState(false);
    const [photos, setPhotos] = useState([]);
    useEffect(() => {
        async function fetch() {
            setIsLoding(true);
            try {
                const data = await GetUserPhotosTop3();
                setPhotos(data);
            } catch {

            } finally {
                setIsLoding(false);
            }

            return;
        };
        fetch();
    }, [])

    return (

        <div className="bg-[white] border-2 w-[100%] h-[200px] rounded-md p-5 mt-5">
            <h3 className="text-gray-500 font-bold">Photos</h3>
            {
                isLoading ? <Spinner></Spinner> :
                    <div className='flex gap-5 w-[100%] h-[80%] overflow-scroll'>
                        {
                            photos.map((image) =>
                                <img key={image.id} src={`https://localhost:7279//userPhotos/${image.imagePath}`} className="rounded-lg h-[100%]" width={100}></img>)

                        }
                    </div>

            }
        </div>

    )
}
