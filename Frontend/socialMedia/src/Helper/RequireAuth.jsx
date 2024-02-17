import { User } from '../Context/UserContext'
import React, { useContext, useEffect } from 'react'
import { Navigate, Outlet } from 'react-router-dom';
import Cookies from 'universal-cookie';

export default function RequireAuth() {
    //protected route
    const cookie = new Cookies();
    const token = cookie.get("bearer");
  
    return token ? <Outlet></Outlet> : <Navigate to="login"></Navigate>
}
