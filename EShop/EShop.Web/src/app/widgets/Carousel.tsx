import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import MobileStepper from "@material-ui/core/MobileStepper";
import Button from "@material-ui/core/Button";
import ArrowLeftIcon from "@material-ui/icons/KeyboardArrowLeft";
import ArrowRightIcon from "@material-ui/icons/KeyboardArrowRight";
import SwipeableViews from "react-swipeable-views";
import DialogTitle from "@material-ui/core/DialogTitle";
import Dialog from "@material-ui/core/Dialog";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    textAlign: "center",
  },
  header: {
    display: "flex",
    alignItems: "center",
    height: 50,
    paddingLeft: theme.spacing(4),
  },
  img: {
    maxHeight: "65vh",
    objectFit: "contain",
  },
}));

type SwipeableTextMobileStepperProps = {
  images: string[];
  title: string;
};
function SwipeableTextMobileStepper({
  images,
  title,
}: SwipeableTextMobileStepperProps) {
  const classes = useStyles();
  const [activeStep, setActiveStep] = React.useState(0);
  const maxSteps = images.length;
  const handleNext = () => setActiveStep((step) => step + 1);
  const handleBack = () => setActiveStep((step) => step - 1);
  const handleStepChange = (step: number) => setActiveStep(step);
  const isFirstStep = activeStep === 0;
  const isLastStep = activeStep === maxSteps - 1;

  return (
    <div className={classes.root}>
      <SwipeableViews
        index={activeStep}
        onChangeIndex={handleStepChange}
        enableMouseEvents
      >
        {images.map((step, index) => (
          <div key={step}>
            {Math.abs(activeStep - index) <= 2 ? (
              <img className={classes.img} src={step} alt={title} />
            ) : null}
          </div>
        ))}
      </SwipeableViews>
      <MobileStepper
        steps={maxSteps}
        position="static"
        variant="dots"
        activeStep={activeStep}
        nextButton={
          <Button size="small" onClick={handleNext} disabled={isLastStep}>
            Next
            <ArrowRightIcon />
          </Button>
        }
        backButton={
          <Button size="small" onClick={handleBack} disabled={isFirstStep}>
            <ArrowLeftIcon />
            Back
          </Button>
        }
      />
    </div>
  );
}

type CarouselProps = {
  title: string;
  images: string[];
  open: boolean;
  onClose: () => void;
};
export default function Carousel({
  title,
  images,
  open,
  onClose,
}: CarouselProps) {
  return (
    <Dialog onClose={onClose} open={open}>
      <DialogTitle>{title}</DialogTitle>
      <SwipeableTextMobileStepper title={title} images={images} />
    </Dialog>
  );
}
