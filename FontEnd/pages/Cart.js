import React from "react";
import Header from "../components/Header/Header";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
import { makeStyles } from "@material-ui/core/styles";
import HeaderLinks from "../components/Header/HeaderLinks";
import { ENDPOINTS, createAPIEndpoint } from "../api";
import { useEffect } from "react";
import { useState } from "react";
import ClothesCartTable from "../pages-sections/Clothes-Sections/ClothesCartTable";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, IconButton } from "@material-ui/core";
import { Close } from "@material-ui/icons";
import numeral from "numeral";
const dashboardRoutes = [];
const useStyles = makeStyles(styles);
export default function ClothesDetail()
{
  const classes = useStyles();
  const [clothes, setClothes] = useState([]);
  const [voucher, setVoucher] = useState();
  const [modal, setModal] = useState(false);
  const [addNotification, setAddNotification] = useState();
  const [sumMoney, setSumMoney] = useState(0)
  useEffect(()=> {
    createAPIEndpoint(ENDPOINTS.Order)
    .fetch()
    .then(res => {
        setClothes(res.data)
    })
  },[])
  const UpdateStatus = () => {
      var orderId = []
      var clothesName = []
      var sumMoney = 0;
      var checkedBoxes = document.querySelectorAll('input[name=pick]:checked');
      sumMoney = document.querySelector('h3[name=sumMoney]').textContent
      for (var i = 0; i < checkedBoxes.length; i++) {
        orderId.push(parseInt(checkedBoxes[i].value))
        clothesName.push(document.querySelector('td[id=name'+checkedBoxes[i].value+']').textContent)
      }
      if(orderId.length !== 0)
      {
        if (voucher !== undefined)
        {
          createAPIEndpoint(ENDPOINTS.Voucher)
          .postWithVoucher(voucher.id)
        }
        createAPIEndpoint(ENDPOINTS.UpdateOrder)
        .put({
              ListOrder:orderId,
              sum: sumMoney,
              ListClothesName: clothesName
          })
        .then(res => {
          if(res.data === true)
          {
            window.location.reload(false);
          }
        })
      } 
  }
  const submitVoucher = () => {
    var codeVoucher = document.querySelector('input[name=voucher]').value;
    if (codeVoucher === "")
    {
      setModal(true);
      setAddNotification("Voucher not valid");
    }
    else
    {
      createAPIEndpoint(ENDPOINTS.Voucher)
      .fetchWithVoucher(codeVoucher)
      .then(res => {
        if(res.data.id == 0)
        {
          setModal(true);
          setAddNotification("Voucher not valid");
        }
        else
        {
          if(res.data.amount == 0)
          {
            setModal(true);
            setAddNotification("Voucher end of use");
          }
          else
          {
            if(res.data.hasUsed)
            {
              setModal(true);
              setAddNotification("You already use this voucher");
            }
            else
            {
              const tds = document.querySelectorAll('td.'+res.data.type);
              tds.forEach(data =>{
                  var price = parseInt(data.textContent.replaceAll(',',''))
                  price -= price * (res.data.discount*0.01)
                  data.textContent = numeral(price).format('0,0') + " VNĐ"
              })
              setModal(true);
              setAddNotification("Voucher apply successful");
              setVoucher(res.data)
            }
          }
        }
        
      })
    }
  }
  const UpdateSumMoney = (money) => {
    setSumMoney(sumMoney + money)
  }
  return (
        <div>
        <style>{"td{border-right:1px solid black;border-bottom:1px solid black;}"}</style>
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
        <div style={{ marginTop: "6%", marginLeft: "150px" }}>
          <input style={{marginTop:"30px"}} type="text" name="voucher"></input>
          <Button color="primary" round="true" onClick={submitVoucher}> Xác nhận Voucher</Button>
          <table style={{ width: "90%", border: "1px solid black" }} id="table">
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
                    {data && <ClothesCartTable clothes={data} UpdateSumMoney={UpdateSumMoney}/>}
                  </tr>
                );
              })}
            </tbody>
          </table>
          <h3 name="sumMoney">Tổng tiền: {numeral(sumMoney).format('0,0')} VNĐ</h3>
          <Button color="primary" round="true"  onClick={UpdateStatus} style={{marginLeft:"80%", marginTop:"5%"}}> Xác nhận thanh toán</Button>
        </div>
        <Dialog
            classes={{
            root: classes.center,
            paper: classes.modal
            }}
            open={modal}
            
            keepMounted
            onClose={() => setModal(false)}
            aria-labelledby="modal-slide-title"
            aria-describedby="modal-slide-description"
        >
            <DialogTitle
            id="classic-modal-slide-title"
            disableTypography
            className={classes.modalHeader}
            >
            <IconButton
                className={classes.modalCloseButton}
                key="close"
                aria-label="Close"
                color="inherit"
                onClick={() => setModal(false)}
            >
                <Close className={classes.modalClose} />
            </IconButton>
            <h4 className={classes.modalTitle}>Notification</h4>
            </DialogTitle>
            <DialogContent
            id="modal-slide-description"
            >
            <h5>{addNotification}</h5>
            </DialogContent>
            <DialogActions
            className={classes.modalFooter + " " + classes.modalFooterCenter}
            >
            <Button onClick={() => setModal(false)} color="default" style={{backgroundColor:"green"}}>
                Yes
            </Button>
            </DialogActions>
        </Dialog>
      </div>
  )
}