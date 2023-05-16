import React, { useEffect, useState } from "react";
// core components
import Header from "/components/Header/Header.js";
import Footer from "/components/Footer/Footer.js";
import HeaderLinks from "/components/Header/HeaderLinks.js";

import { useRouter } from "next/router";
import { ENDPOINTS, createAPIEndpoint } from "../../api";
import ClothesDetail from "../../pages-sections/Clothes-Sections/ClothesDetail"; 

const dashboardRoutes = [];
export default function ClothesItem() {
  const [item, setItem] = useState(null);
  const router = useRouter()
  const { ClothesNameAndID } = router.query
  const ClothesID = ClothesNameAndID.split("-")[1]
  useEffect(()=> {
    createAPIEndpoint(ENDPOINTS.getClothes)
    .fetchWithName(ClothesID)
    .then(res => {
      setItem(res.data)
    })
  },[])
  return (
    <div>
        <Header
          color="dark"
          routes={dashboardRoutes}
          brand="NextJS Material Kit"
          rightLinks={<HeaderLinks />}
          fixed
          changeColorOnScroll={{
            height: 400,
            color: "dark"
          }}          
        />
        <div style={{marginTop:"10%", marginLeft:"150px"}}>
            {item && <ClothesDetail clothes={item}/>}
        </div>
      <Footer />
      </div>
  );
}
