import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom"
import Login from './pages/Login'
import Register from './pages/Register'
import Home from './pages/Home'
import AppLayout from './compontes/AppLayout'
import PageNotFound from './pages/PageNotFound'


function App() {


  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login></Login>}></Route>
        <Route path="/signin" element={<Register></Register>}></Route>
        <Route element={<AppLayout></AppLayout>}>
          <Route index element={<Navigate replace to="/login" />} />
          <Route path='/home' element={<Home></Home>}></Route>
          <Route path='*' element={<PageNotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
