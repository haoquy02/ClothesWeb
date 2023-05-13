import React from "react";
import Header from "../components/Header/Header";
import HeaderLinks from "../components/Header/HeaderLinks";
import { ENDPOINTS, createAPIEndpoint } from "../api";
import { useEffect } from "react";
import { useState } from "react";
import ClothesCartTable from "../pages-sections/Clothes-Sections/ClothesCartTable";
import { Button } from "@material-ui/core";
const dashboardRoutes = [];



export default function ClothesDetail()
{
  const [clothes, setClothes] = useState([]);
  

  useEffect(()=> {
    createAPIEndpoint(ENDPOINTS.Order)
    .fetch()
    .then(res => {
        setClothes(res.data)
    })
  },[])
  const UpdateStatus = () => {
      var orderId = []
      var checkedBoxes = document.querySelectorAll('input[name=pick]:checked');
      for (var i = 0; i < checkedBoxes.length; i++) {
        orderId.push(parseInt(checkedBoxes[i].value))
      }
      createAPIEndpoint(ENDPOINTS.UpdateOrder)
      .put(orderId)
      .then(res => {
        if(res.data === true)
        {
          window.location.reload(false);
        }
    })
  }
  return (
      <><style>{"td{border-right:1px solid black;border-bottom:1px solid black;}"}</style>
      <div>
      <Header
        color="dark"
        routes={dashboardRoutes}
        brand="NextJS Material Kit"
        rightLinks={<HeaderLinks />}
        fixed
        changeColorOnScroll={{
          height: 400,
          color: "white"
        }} />
      <div style={{ marginTop: "10%", marginLeft: "150px" }}>
        <table style={{ width: "90%", border: "1px solid black" }}>
          <tbody>
            <tr>
              <td></td>
              <td>Hình ảnh sản phẩm</td>
              <td>Tên sản phẩm</td>
              <td>Gía tiền</td>
              <td>Số lượng</td>
              <td>Thành tiền</td>
              <td>Trạng thái</td>
            </tr>
            {clothes.map(data => {
              return (
                <tr key={data.id}>
                  {data && <ClothesCartTable detail={data}/>}
                </tr>
              );
            })}
          </tbody>
        </table>
        <Button color="primary" round="true"  onClick={UpdateStatus} style={{marginLeft:"80%", marginTop:"5%"}}> Xác nhận thanh toán</Button>
      </div>
    </div></>
  )
}