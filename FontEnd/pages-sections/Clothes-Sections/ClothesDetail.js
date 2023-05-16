import React, { forwardRef, useState } from "react";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
import { makeStyles } from "@material-ui/core/styles";
import GridContainer from "/components/Grid/GridContainer.js";
import GridItem from "/components/Grid/GridItem.js";
import Card from "/components/Card/Card.js";
// nodejs library that concatenates classes
import classNames from "classnames";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, IconButton, Slide } from "@material-ui/core";
import { AddShoppingCartSharp, Close } from "@material-ui/icons";
import { ENDPOINTS, createAPIEndpoint } from "../../api";
const useStyles = makeStyles(styles);
const BASE_URL = "/img/clothes/"
export default function ClothesDetail(detail) {
    console.log(detail)
    const classes = useStyles();
    const [quantity,setQuantity] = useState(1);
    const [modal, setModal] = useState(false);
    const [addNotification, setAddNotification] = useState();
    const size = detail.clothes.size.split(",");

    const imageClasses = classNames(
        classes.imgRaised,
        classes.imgFluid
      );
    const handleQuantity = e =>{
        const {value} = e.target
        if (value > detail.clothes.quantity)
        {
            document.querySelector('input[name="quantity"]').value = detail.clothes.quantity
        }
        else if (value < 0)
        {
            document.querySelector('input[name="quantity"]').value = 0
        }
        setQuantity(value)
    }
    const addToCart = () => {
        var size = document.querySelector('input[name="size"]:checked').value;
        createAPIEndpoint(ENDPOINTS.Order)
        .post({
            ClothesId: detail.clothes.id,
            Quantity: quantity,
            Size: size,
            Status: 1
        })
        .then(res =>{
            setAddNotification(res.data)
            setModal(true)
        })
    }
    return (
        <GridContainer>
            <GridItem xs={12} sm={12} md={3}>
                <Card plain>
                    <img
                    src= {BASE_URL + detail.clothes.image}
                    alt="..."
                    className={imageClasses}
                    />
                </Card>
                <div>- {detail.clothes.material}</div>
                <div>- {detail.clothes.description}</div>
                <div>- {detail.clothes.gender}</div>
            </GridItem>
            <GridItem xs={12} sm={12} md={6}>
                <Card plain>
                   <h1>{detail.clothes.clothesName}</h1>
                   <h2>${detail.clothes.prices}.000 VNĐ</h2>
                </Card>
                <div style={{color:"red"}} hidden={detail.clothes.quantity < 5 && detail.clothes.quantity > 0 ? false:true}>Only {detail.clothes.quantity} left</div>
                <br/>
                <input type="number" id="quantity" name="quantity" defaultValue={1} min="1" max={detail.clothes.quantity} style={{width: 120, height: 30}} onChange={handleQuantity}/>
                <br/>
                {size.map((item,index) => {
                    return (
                        <div key={item}>
                            <input type="radio" id={item} name="size" value={item} defaultChecked={index === 0}/>
                            <label htmlFor={item}>{item}</label><br/>
                        </div>
                    )
                })}
                 <br/>
                 <div style={{color:"red"}} hidden={detail.clothes.quantity == 0 ? false:true}>Out of stock</div>
                <Button color="primary" style={{marginTop: "10px"}} onClick={addToCart} disabled={detail.clothes.quantity == 0 ? true:false}><AddShoppingCartSharp/> ADD TO CART</Button>
            </GridItem>
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
                    className={classes.modalBody}
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
        </GridContainer>
    )
}