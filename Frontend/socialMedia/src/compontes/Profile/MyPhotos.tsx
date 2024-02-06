import { GetUserPhotos } from '../../Helper/PhotoApi';
import React, { useEffect, useState } from 'react'

export default function MyPhotos() {
  const [isLoading, setIsLoding] = useState(false);
  const [photos, setPhotos] = useState([]);
  useEffect(() => {
    async function fetch() {
      setIsLoding(true);
      try {
        const data = await GetUserPhotos();
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
    <div className='m-10 bg-[white] p-5 rounded-lg border-2'>
      <h3 className='mb-3 font-bold text-gray-500 text-[20px]'>MyPhotos</h3>
      <hr></hr>
      { 
        isLoading ? "" :
          <div className='grid md:grid-cols-4 mt-10 gap-5 h-[1000px] sm:grid-cols-2 grid-cols-1'>
            {
              photos.map((image) =>
                <img key={image.id} src={`https://localhost:7279//userPhotos/${image.imagePath}`} className="rounded-lg h-[100%] w-[80%] hover:bg-gray-500 shadow-lg border-2 p-3" width={100}></img>)

            }
          </div>
      }
    </div>
  )
}
