import React from 'react';

const HeaderDropdownToggle = React.forwardRef(({ children, onClick }, ref) => (
  <div ref={ref} className='kt-header__topbar-wrapper' onClick={onClick}>
    {children}
  </div>
));

export default HeaderDropdownToggle;
