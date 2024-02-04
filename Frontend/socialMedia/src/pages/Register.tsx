import React, { useEffect } from 'react'
import './Login.css'
import '/image/loginBack.jpg'
import { Link } from 'react-router-dom'

export default function Register() {   

    return (
        <div className='w-[100%]  h-[100vh] flex items-center justify-center'>
            <div className='login w-[80%] bg-[white] flex rounded-lg'>
                <div className='loginform w-[50%] p-[40px] flex flex-col gap-5'>
                    <div className='m-auto'>
                        <h3 className='text-[30px] mb-2  font-bold'>MetaFans</h3>
                        <h1 className='text-[60px] mb-3 font-bold'>WELCOME TO YOU</h1>
                        <p>Please enter your details.</p>
                    </div>
                    <form className='flex flex-col gap-3 m-auto '>
                        <input className='inp' type="text" placeholder='Username'></input>
                        <input className='inp' type="text" placeholder='Email'></input>
                        <input className='inp' type="password" placeholder='Password'></input>
                        <input className='inp' type="password" placeholder='Confirm password'></input>
                        <input className='inp' type="text" placeholder='Phone'></input>

                        <div className='flex justify-between w-[100%]'>
                            <div className='flex items-center gap-1 mr-5'>
                                <input id="remember" type='checkbox'></input>
                                <label htmlFor="remember" className='cursor-pointer '>Remember me</label>
                            </div>
                            <p className='cursor-pointer'>Lost your password?</p>
                        </div>
                        <button className='SubBut m-auto bg-gray-800 text-white p-3 rounded-md'>Register</button>
                        <Link to="/login" className='cursor-pointer m-auto'>I,m already have account.</Link>
                    </form>
                </div>
                <section className='ImgLogin w-[50%] flex-col justify-center items-center gap-5'>
                    <h1 className='text-[white] font-bold text-[30px]'>Make an awesome experience</h1>
                    <h3 className='text-[white] text-[18px]'>Join our world and share your events with us &#128525;.</h3>
                </section>
            </div>
        </div>
    )
}
