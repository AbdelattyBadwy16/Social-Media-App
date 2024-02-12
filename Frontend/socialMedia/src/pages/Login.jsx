import React, { useContext, useState } from 'react'
import './Login.css'
import '/image/loginBack.jpg'
import { Link, useNavigate } from 'react-router-dom'
import { LoginUser } from '../Helper/AccountApi';
import Spinner from '../compontes/Spinner';
import { User } from '../Context/UserContext';
import Cookies from 'universal-cookie';


export default function Login() {

    const [UserName, setUserName] = useState("");
    const [Password, setPassword] = useState("");
    const [errContent, setErrContent] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const nav = useNavigate();
    const UserContext = useContext(User);
    const cookie = new Cookies();
    //validation and submit account
    async function handelSubmit(e) {
        e.preventDefault();
        if (!UserName.length || !Password.length) {
            setErrContent("Email or Password Mustn't be empty.");
            return;
        }
        try {
            setIsLoading(true);
            const res = await LoginUser({ UserName, Password });
            const data = await res.json();
            UserContext.setAuth(data);

            if (res.ok) {
                cookie.remove("bearer");
                cookie.remove("userName");
                cookie.remove("image");
                cookie.remove("id");
                cookie.set("bearer", data.token);
                cookie.set("userName", data.userName);
                cookie.set("image", data.iconImagePath);
                cookie.set("id", data.id);
                setTimeout(() => {
                    nav("/home");
                }, 3000);

            } else setErrContent("Invaild username or password.");

        } finally {
            setIsLoading(false);
        }

        return;
    }


    return (
        <div className='w-[100%]  h-[100vh] flex items-center justify-center'>
            <div className='login w-[80%] bg-[white] flex rounded-lg'>
                <div className='loginform w-[50%] p-[40px] flex flex-col gap-5'>
                    <div className='m-auto'>
                        <h3 className='title text-[30px] mb-2  font-bold'>MetaFans</h3>
                        <h1 className='welcome text-[70px] mb-3 font-bold'>WELCOME BACK</h1>
                        <p>Welcome back! Please enter your details.</p>
                    </div>
                    <form className='flex flex-col gap-3 m-auto ' onSubmit={handelSubmit}>
                        <input value={UserName} onChange={(e) => setUserName(e.target.value)} className='inp' type="text" placeholder='email'></input>
                        <input value={Password} onChange={(e) => setPassword(e.target.value)} className='inp' type="password" placeholder='Password'></input>
                        <div className='flex justify-between w-[100%]'>
                            <div className='flex items-center gap-1 mr-5'>
                                <input id="remember" type='checkbox'></input>
                                <label htmlFor="remember" className='cursor-pointer'>Remember me</label>
                            </div>
                            <p className='cursor-pointer'>Lost your password?</p>
                        </div>
                        <p className='text-center text-red-800 font-bold'>{errContent}</p>
                        <button className='SubBut m-auto bg-gray-800 text-white p-3 rounded-md'>{isLoading ? <Spinner></Spinner> : "Login"}</button>
                        <Link to="/signin" className='cursor-pointer m-auto'>I,m not have account yet ?</Link>
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
