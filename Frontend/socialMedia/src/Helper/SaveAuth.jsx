import { User } from '../Context/UserContext'
import React, { useContext, useEffect } from 'react'
import { Navigate, Outlet } from 'react-router-dom';
import Cookies from 'universal-cookie';

export default function RequireAuth() {
    // check if token found or not
    const user = useContext(User);
    const cookie = new Cookies();
    const userName = cookie.get("userName");
    const token = cookie.get("bearer");
    const iconImage = cookie.get("image");
    useEffect(() => {
        user.setAuth((prev) => {
            return { userName, token, iconImage }
        })
    }, [])
    return !token ? <Outlet></Outlet> : <Navigate to="/home"></Navigate>
}
