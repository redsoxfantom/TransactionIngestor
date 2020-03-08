from enum import Enum

class InputType(Enum):
    STANDARD_FORMAT_JSON = 0,
    WELLS_FARGO_CSV = 1

class OutputType(Enum):
    STANDARD_FORMAT_JSON = 0