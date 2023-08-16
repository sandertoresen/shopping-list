import styled from 'styled-components';

export const StyledBaseInput = styled.input`
    margin: 1%;
    font-size: 1.5rem;
    border-radius: 5px;
`;

export const StyledPrimaryInput = styled(StyledBaseInput)`
    width: 10%;
    margin-right: .5%;
`;

export const StyledSecondInput = styled(StyledBaseInput)`
    width: 5%;
`;