import { GetUserData } from '../../Helper/ProfileApi';
import React, { useContext, useEffect, useState } from 'react'
import Cookies from 'universal-cookie';
import Spinner from '../Spinner';
import { ToastContainer, toast } from 'react-toastify';
import { UpdateAccount } from '../../Helper/AccountApi';
import { User } from '../../Context/UserContext';

export default function Setting() {
    const [FirstNameCheck, setFirstNameCheck] = useState(false);
    const [LastNameCheck, setLastNameCheck] = useState(false);
    const [PasswordCheck, setPasswordCheck] = useState(false);
    const [nickNameCheck, setNickNameCheck] = useState(false);
    const [BirhDateCheck, setBirhDateCheck] = useState(false);
    const [JopTitleCheck, setJopTitleCheck] = useState(false);
    const [PhoneNumberCheck, setPhoneNumberCheck] = useState(false);
    const [CountryCheck, setCountryCheck] = useState(false);

    let [FirstName, setFirstName] = useState("");
    let [LastName, setLastName] = useState("");
    let [Password, setPassword] = useState("*********");
    let [nickName, setnickName] = useState("");
    let [BirhDate, setBirhDate] = useState("");
    let [JopTitle, setJopTitle] = useState("");
    let [PhoneNumber, setPhoneNumber] = useState("");
    let [Country, setCountry] = useState("");
    let [oldPassword, setOldPassword] = useState("");

    const [isLoading, setIsLoading] = useState(false);
    const [userData , setUserData] = useState<any>({});
    //get user data
    useEffect(() => {
        setIsLoading(true);

        async function fetch() {
            try {
                const cookie = new Cookies();
                const id = cookie.get("id");
                const data = await GetUserData(id);
                setFirstName(data.firstName);
                setLastName(data.lastName);
                setnickName(data.nickName);
                setBirhDate(data.birthDate);
                setJopTitle(data.jopTitle);
                setPhoneNumber(data.phoneNumber);
                setCountry(data.country);
                setUserData(data);
            } finally {
                setIsLoading(false);
            }
        }
        fetch();
    }, []);


    // handelSubmit
    async function Fetch() {
        const res = await UpdateAccount({ FirstName, LastName, Password, nickName, BirhDate, JopTitle, PhoneNumber, Country, oldPassword })
        if (typeof (res) == "string") {
            toast(res);
        } else {
            toast("Account Updated Successfuly.");
            const context : any = useContext(User);
            const cookie = new Cookies();
            const userId = cookie.get("id");
            const data = await GetUserData(userId);
            context.setUser(data);
            context.setStatus(!context.status);
        }
        return;
    }

    async function handelSubmit(e : any) {
        e.preventDefault();
        if (oldPassword == "") {
            toast("Old Password Required.");
            return;
        }
        if (!FirstNameCheck) FirstName = userData.firstName;
        if (!LastNameCheck) LastName = userData.lastName;
        if (!nickNameCheck) nickName = userData.nickName;
        if (!BirhDateCheck) BirhDate = userData.birthDate;
        if (!JopTitleCheck) JopTitle = userData.jopTitle;
        if (!PhoneNumberCheck) PhoneNumber = userData.phoneNumber;
        if (!CountryCheck) Country = userData.country;
        if (!PasswordCheck) Password = oldPassword;

        Fetch();
        return 0;
    }



    return (
        <div className='m-10 bg-[white] p-5 rounded-lg border-2'>
            <h3 className='mb-3 font-bold text-gray-500 text-[20px]'>Setting</h3>
            <hr></hr>
            {
                isLoading ? <Spinner></Spinner> :
                    <>
                        <h1 className='font-bold underline mt-2'>Check the box of the proparty to change it .</h1>
                        <form className='m-10 grid grid-cols-1 lg:grid-cols-2 gap-5' onSubmit={handelSubmit}>
                            <div className='flex md:flex-row flex-col items-center gap-3'>
                                <label>FirstName :</label>
                                <input value={FirstName} placeholder='FirstName' className={`shadow-lg p-2 rounded-lg ${!FirstNameCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setFirstName(e.target.value)} value={FirstName} placeholder='FirstName' className={`shadow-lg p-2 rounded-lg ${!FirstNameCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setFirstNameCheck(!FirstNameCheck)} className={` ${FirstNameCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setFirstNameCheck(!FirstNameCheck)} className={` ${FirstNameCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>LastName :</label>
                                <input value={LastName} placeholder='LastName' className={`shadow-lg p-2 rounded-lg ${!LastNameCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setLastName(e.target.value)} value={LastName} placeholder='LastName' className={`shadow-lg p-2 rounded-lg ${!LastNameCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setLastNameCheck(!LastNameCheck)} className={` ${LastNameCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setLastNameCheck(!LastNameCheck)} className={` ${LastNameCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>Password :</label>
                                <input value={Password} placeholder='Password' className={`shadow-lg p-2 rounded-lg ${!PasswordCheck ? "" : "hidden"} `} disabled type='password'></input>
                                <input onChange={(e) => setPassword(e.target.value)} value={Password} placeholder='Password' className={`shadow-lg p-2 rounded-lg ${!PasswordCheck ? "hidden" : ""} `} type='password'></input>
                                <input readOnly onClick={() => setPasswordCheck(!PasswordCheck)} className={` ${PasswordCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setPasswordCheck(!PasswordCheck)} className={` ${PasswordCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>NickName :</label>
                                <input value={nickName} placeholder='nickName' className={`shadow-lg p-2 rounded-lg ${!nickNameCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setnickName(e.target.value)} value={nickName} placeholder='nickName' className={`shadow-lg p-2 rounded-lg ${!nickNameCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setNickNameCheck(!nickNameCheck)} className={` ${nickNameCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setNickNameCheck(!nickNameCheck)} className={` ${nickNameCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>BirhDate :</label>
                                <input value={BirhDate} placeholder='BirhDate' className={`shadow-lg p-2 rounded-lg ${!BirhDateCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setBirhDate(e.target.value)} value={BirhDate} placeholder='BirhDate' className={`shadow-lg p-2 rounded-lg ${!BirhDateCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setBirhDateCheck(!BirhDateCheck)} className={` ${BirhDateCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setBirhDateCheck(!BirhDateCheck)} className={` ${BirhDateCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col  md:flex-row flex-col items-center gap-3'>
                                <label>JopTitle :</label>
                                <input value={JopTitle} placeholder='JopTitle' className={`shadow-lg p-2 rounded-lg ${!JopTitleCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setJopTitle(e.target.value)} value={JopTitle} placeholder='JopTitle' className={`shadow-lg p-2 rounded-lg ${!JopTitleCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setJopTitleCheck(!JopTitleCheck)} className={` ${JopTitleCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setJopTitleCheck(!JopTitleCheck)} className={` ${JopTitleCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>PhoneNumber :</label>
                                <input value={PhoneNumber} placeholder='PhoneNumber' className={`shadow-lg p-2 rounded-lg ${!PhoneNumberCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setPhoneNumber(e.target.value)} value={PhoneNumber} placeholder='PhoneNumber' className={`shadow-lg p-2 rounded-lg ${!PhoneNumberCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setPhoneNumberCheck(!PhoneNumberCheck)} className={` ${PhoneNumberCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setPhoneNumberCheck(!PhoneNumberCheck)} className={` ${PhoneNumberCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>Country :</label>
                                <input value={Country} placeholder='CountryCheck' className={`shadow-lg p-2 rounded-lg ${!CountryCheck ? "" : "hidden"} `} disabled type='text'></input>
                                <input onChange={(e) => setCountry(e.target.value)} value={Country} placeholder='CountryCheck' className={`shadow-lg p-2 rounded-lg ${!CountryCheck ? "hidden" : ""} `} type='text'></input>
                                <input readOnly onClick={() => setCountryCheck(!CountryCheck)} className={` ${CountryCheck ? "" : "hidden"}`} type="checkbox" ></input>
                                <input readOnly onClick={() => setCountryCheck(!CountryCheck)} className={` ${CountryCheck ? "hidden" : ""}`} checked type="checkbox" ></input>
                            </div>
                            <div className='flex items-center gap-3'>
                                <label>Update BackGround :</label>
                                <input type='file'></input>
                            </div>
                            <div className='flex  md:flex-row flex-col items-center gap-3'>
                                <label>OldPassword :</label>
                                <input value={oldPassword} onChange={(e) => setOldPassword(e.target.value)} placeholder='OldPassword' className={`shadow-lg p-2 rounded-lg `} type='password'></input>
                                <p className='text-red-500 text-[20px]'>*</p>
                            </div>
                            <div className='flex items-center gap-3 justify-end w-[100%]'>
                                <input type='submit' className='bg-gray-300 shadow-lg p-2 rounded-lg'></input>
                            </div>
                        </form>
                    </>
            }
            <ToastContainer autoClose={1000}></ToastContainer>
        </div>
    )
}
