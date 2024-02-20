import { User } from '../Context/UserContext'
import React, { useContext, useEffect } from 'react'
import { Navigate, Outlet } from 'react-router-dom';
import Cookies from 'universal-cookie';

export default function RequireAuth() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    // check if token found or not
    return !token ? <Outlet></Outlet> : <Navigate to="/home"></Navigate>
}
