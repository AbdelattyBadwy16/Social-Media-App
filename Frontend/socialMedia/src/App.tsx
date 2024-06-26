import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom"
import React from "react"
import Login from './pages/Login'
import Register from './pages/Register'
import Home from './pages/Home'
import AppLayout from './compontes/AppLayout'
import PageNotFound from './pages/PageNotFound'
import Members from "./pages/Members"
import Photos from "./pages/Photos"
import Profile from "./pages/Profile"
import Activity from "./compontes/Profile/Activity"
import MyPhotos from "./compontes/Profile/MyPhotos"
import RequireAuth from "./Helper/RequireAuth"
import SaveAuth from "./Helper/SaveAuth"
import Following from "./compontes/Profile/Following"
import Follower from "./compontes/Profile/Follower"
import Setting from "./compontes/Profile/Setting"
import AllMember from "./compontes/Home/AllMember"
import MyFavourite from "./compontes/Home/MyFavourite"
import MyFollowing from "./compontes/Home/MyFollowing"


function App() {


  return (
    <BrowserRouter>
      <Routes>
        {/*public Routes */}
        <Route element={<SaveAuth></SaveAuth>}>
          <Route path="/login" element={<Login></Login>}></Route>
          <Route path="/signin" element={<Register></Register>}></Route>
          {/*Private Routes */}
        </Route>
        <Route element={<RequireAuth></RequireAuth>}>
          <Route element={<AppLayout></AppLayout>}>
            <Route index element={<Navigate replace to="/login" />} />
            <Route path='/home' element={<Home></Home>}>
              <Route index element={<AllMember></AllMember>}></Route>
              <Route index path='MyFavourite' element={<MyFavourite></MyFavourite>}></Route>
              <Route index path='activiy' element={<AllMember></AllMember>}></Route>
              <Route index path='Following' element={<MyFollowing></MyFollowing>}></Route>
            </Route>
            <Route path='/members' element={<Members></Members>}></Route>
            <Route path='/photos' element={<Photos></Photos>}></Route>
            <Route path='/Profile' element={<Profile></Profile>}>
              <Route index element={<Activity></Activity>}></Route>
              <Route path="Activity" element={<Activity></Activity>}></Route>
              <Route path="Photos" element={<MyPhotos></MyPhotos>}></Route>
              <Route path="Following" element={<Following></Following>}></Route>
              <Route path="Follower" element={<Follower></Follower>}></Route>
              <Route path="Setting" element={<Setting></Setting>}></Route>
            </Route>
            <Route path='*' element={<PageNotFound />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
