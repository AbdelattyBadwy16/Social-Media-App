import React, { useState } from 'react'
import './Login.css'
import '/image/loginBack.jpg'
import { Link, useNavigate } from 'react-router-dom'
import { CheckName, CheckPassword } from "../Helper/Validation"
import { RegisterNewUser } from "../Helper/AccountApi"
import Spinner from '../compontes/Spinner'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';



export default function Register() {

    const [FirstName, setFirstName] = useState("");
    const [LastName, setLastName] = useState("");
    const [UserName, setUserName] = useState("");
    const [Email, setEmail] = useState("");
    const [Password, setPassword] = useState("");
    const [ConfirmPassword, setConfirmPassword] = useState("");
    const [PhoneNumber, setPhone] = useState("");
    const [Country, setCountry] = useState("");
    const [Gender, setGender] = useState("male");
    const [errContent, setErrContent] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const nav = useNavigate();

    async function handelSubmit(e : any) {
        e.preventDefault();

        if (!CheckName(FirstName) || !CheckName(LastName)) {
            setErrContent("First and Second Name shouldn't be empty.");
            return;
        }

        if (!UserName.length) {
            setErrContent("UserName shouldn't be empty.");
            return;
        }

        if (!Email.length) {
            setErrContent("Email shouldn't be empty.");
            return;
        }

        if (Password.length < 8) {
            setErrContent("Password must have length at least 8.");
            return;
        }

        if (!CheckPassword(Password)) {
            setErrContent("Password must have at least one non alphanumeric char and one digit ('0'-'9') and one uppercase ('A'-'Z')");
            return;
        }

        if (!PhoneNumber.length) {
            setErrContent("Phone shouldn't be empty.");
            return;
        }

        if (!Country.length) {
            setErrContent("country shouldn't be empty.");
            return;
        }

        if (Password != ConfirmPassword) {
            setErrContent("Password and Confirm Password must be the same.");
            return;
        }
        setErrContent("");

        let res = await RegisterNewUser({ FirstName, LastName, UserName, Email, Password, PhoneNumber, Country, Gender });

        if (res.ok) {
            toast("Email Created Successfuly!");
            setIsLoading(true);
            setTimeout(() => {
                nav("/login");
                setIsLoading(false);
            }, 5000);
        } else setErrContent("User has already exists.");


        return;
    }

    return (
        <div className=' w-[100%]  h-[100vh] flex items-center justify-center'>
            <div className='login w-[80%] bg-[white] flex rounded-lg'>
                <div className='loginform w-[50%] p-[40px] flex flex-col gap-5'>
                    <div className=' m-auto'>
                        <h3 className='title text-[30px] mb-2  font-bold'>MetaFans</h3>
                        <h1 className='welcome text-[60px] mb-3 font-bold'>WELCOME TO YOU</h1>
                        <p>Please enter your details.</p>
                    </div>
                    <form className='flex flex-col gap-3 m-auto ' onSubmit={handelSubmit}>
                        <div className='flex gap-5'>
                            <input className='inp' value={FirstName} onChange={(e) => setFirstName(e.target.value)} type="text" placeholder='FirstName'></input>
                            <input className='inp' value={LastName} onChange={(e) => setLastName(e.target.value)} type="text" placeholder='SecondName'></input>
                        </div>
                        <input value={UserName} onChange={(e) => setUserName(e.target.value)} className='inp' type="text" placeholder='Username'></input>
                        <input value={Email} onChange={(e) => setEmail(e.target.value)} className='inp' type="text" placeholder='Email'></input>
                        <input value={Password} onChange={(e) => setPassword(e.target.value)} className='inp' type="password" placeholder='Password'></input>
                        <input value={ConfirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} className='inp' type="password" placeholder='Confirm password'></input>
                        <input value={PhoneNumber} onChange={(e) => setPhone(e.target.value)} className='inp' type="text" placeholder='Phone'></input>
                        <div className='flex items-center justify-center'>
                            <input value={Country} onChange={(e) => setCountry(e.target.value)} className='inp' type="text" placeholder='Country'></input>
                            <select className='border-2 rounded-lg ml-5 p-2 shadow-lg' value={Gender} onChange={(e) => setGender(e.target.value)}>
                                <option value="male">Male</option>
                                <option value="female">Female</option>
                            </select>
                        </div>
                        <p className='text-center text-red-800 font-bold'>{errContent}</p>
                        <button className='SubBut m-auto bg-gray-800 text-white p-3 rounded-md'>{isLoading ? <Spinner></Spinner> : "Register"}</button>
                        <Link to="/login" className='cursor-pointer m-auto'>I,m already have account.</Link>
                    </form>
                </div>
                <section className='ImgLogin w-[50%] flex-col justify-center items-center gap-5'>
                    <h1 className='text-[white] font-bold text-[30px]'>Make an awesome experience</h1>
                    <h3 className='text-[white] text-[18px]'>Join our world and share your events with us &#128525;.</h3>
                </section>
            </div>
            <ToastContainer />
        </div>

    )
}
